using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Application.Common.Interfaces;
using Web.Domain.Entities;
using Web.Infrastructure.Identity;
using Web.Infrastructure.Services;
using Web.WebUI.ExtensionsMethods;

namespace Web.WebUI.Areas.Identity.Pages.Account.Manage;
public partial class IndexModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IProfileService _profileService;
    private readonly IAccountService _accountService;
    private readonly IFileService _fileService;

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IProfileService profileService,
        IAccountService accountService,
        IFileService fileService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _profileService = profileService;
        _accountService = accountService;
        _fileService= fileService;
    }

    public string Username { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }


    public class InputModel
    {
        [Required]
        [Display(Name = "Login")]
        [StringLength(20,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Login { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(20,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }
        [BindProperty]
        [Display(Name = "Upload your picture")]
        public IFormFile Picture { set; get; }
        public string PathPicture { get; set; }
    }

    private async Task LoadAsync(ApplicationUser user, Profile profile)
    {
        var userName = await _userManager.GetUserNameAsync(user);
        

        Username = userName;
        Input = new InputModel
        {
            Login = profile.Login,
            FirstName = profile.FirstName,
            LastName= profile.LastName,
            PathPicture = profile.Picture.ShortPath
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        Profile profile= await _profileService.GetProfileByUserId(user.Id);
        await LoadAsync(user, profile);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        Profile profile= await _profileService.GetProfileByUserId(user.Id);
        PathToFile pathToFile = profile.Picture;
        if (!ModelState.IsValid)
        {
            await LoadAsync(user, profile);
            return Page();
        }
        if(Input.Login!= profile.Login &&await _accountService.IsLoginExist(Input.Login))
        {
            ModelState.AddModelError("Input.Login", "This login already exist");
            await LoadAsync(user, profile);
            return Page();
        }
        if (Input.Picture != null)
        {
            PathToFile pathToFileNew =_fileService.SaveFile(await Input.Picture.ConvertToFileModelAsync());
            if (pathToFile!= null)
            {
                pathToFile.ShortPath=pathToFileNew.ShortPath;
                pathToFile.FullPath= pathToFile.FullPath;
            }
            else
            {
                pathToFile= pathToFileNew;
            }
        }
        profile.Login = Input.Login;
        profile.FirstName = Input.FirstName;
        profile.LastName= Input.LastName;
        profile.Picture= pathToFile;
        await _profileService.UpdateProfile(user.Id, profile);
        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your profile has been updated";
        return RedirectToPage();
    }
}
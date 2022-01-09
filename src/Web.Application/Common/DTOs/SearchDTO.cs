using Web.Application.Common.Enums;
using Web.Domain.Interfaces;

namespace Web.Application.Common.DTOs;
public class SearchDTO:IPaginated
{
    public string Title { get; set; }= string.Empty;
    public TypeItemSearch Type { get; set; }= default;
    public string Picture { get; set; }=string.Empty;
    public int Id { get; set; }=0;
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Domain.Interfaces;

namespace Web.Application.Common.Models;
public class PaginatedList<T> where T :  IPaginated
{
    public List<T> Items { get; }
    public int Id { get; set; }
    public int PageSize { get ;set; }
    public bool HasNextPage { get ;set; }
    public PaginatedList(int id, int pageSize,bool hasNextPage) 
    {
        Id = id;
        PageSize = pageSize;
        HasNextPage = hasNextPage;
    }

    public PaginatedList(List<T> items, int id, int pageSize, bool hasNextPage)
    {
        Id = id;
        Items = items;
        PageSize  = pageSize;
        HasNextPage = hasNextPage;

    }


    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int id, int pageSize)
    {
        var items = new List<T>();
        if(id<= 0)
        {
            items= await source.Take(pageSize).ToListAsync();
        }
        else
        {
            items = await source.Where(x=>x.Id > id).Take(pageSize).ToListAsync();   
        }
        bool hasNextPage;
        if(items.Count()>0)
        {
            hasNextPage= await source.AnyAsync(x=>x.Id > items.Last().Id);
        }
        else
        {
            hasNextPage = false;
        }
        return new PaginatedList<T>(items, items.Count() !=0? items.Last().Id: -1, pageSize, hasNextPage);
    }
}
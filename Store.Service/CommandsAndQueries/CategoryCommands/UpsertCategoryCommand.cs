using Microsoft.EntityFrameworkCore;
using Store.Contract.Response.Category;
using Store.Data.Context;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Store.Service.CommandsAndQueries.CategoryCommands;
public class UpsertCategoryCommand
{
    public DateTime CreatedDate { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }


    public Category UpsertCategory()
    {
        Category category = new ()
        {
            CreatedDate = CreatedDate,
            Id = Id,
            Name = Name,
            Description = Description
        };
        return category;
    }
}

public class UpsertCategoryCommandHandler(AppDbContext appDbContext): IRequestHandler<UpsertCategoryCommand, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(UpsertCategoryCommand request, CancellationToken cancelationToken = default)
    {
        Category? category = await GetCategoryAsync(request.Id, cancelationToken);

        if(category == null)
        {
            category = request.UpsertCategory();
            await appDbContext.AddAsync(category, cancelationToken);
        }

        category.Name = request.Name;
        category.Description = request.Description;

        return new CategoryResponse
        {
            CreatedDate = category.CreatedDate,
            Id = category.Id,
            Name = request.Name,
            Description = request.Description,
        };
    }

    private async Task<Category?> GetCategoryAsync(int categoryId,  CancellationToken cancelationToken = default)
        => await appDbContext.Categories.SingleOrDefaultAsync(e => e.Id == categoryId, cancelationToken);
}

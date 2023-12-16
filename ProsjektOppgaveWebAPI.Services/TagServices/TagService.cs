using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProsjektOppgaveWebAPI.Common;
using ProsjektOppgaveWebAPI.Database.Entities;
using ProsjektOppgaveWebAPI.EntityFramework;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Services.Response;
using ProsjektOppgaveWebAPI.Services.TagServices.Models;

namespace ProsjektOppgaveWebAPI.Services.TagServices;

public class TagService : ITagService
{
    private readonly UserManager<IdentityUser> _manager;
    private readonly IGenericRepository<Tag> _tagRepository;
    
    public TagService(UserManager<IdentityUser> userManager, IGenericRepository<Tag> tagRepository)
    {
        _manager = userManager;
        _tagRepository = tagRepository;
    }
    
    public async Task<ResponseService<Tag>> Create(CreateTagHttpPostModel vm)
    {
        Tag dbRecord = await _tagRepository.GetAll()
            .FirstOrDefaultAsync(x=>x.Name == vm.Name && x.User.UserName == vm.UserName);

        if (dbRecord != null)
        {
            ResponseService<long>.Error(Errors.TAG_ALREADY_EXISTS);
        }
        
        dbRecord = new Tag
        {
            Name = vm.Name,
            User = await _manager.FindByNameAsync(vm.UserName)
        };

        try
        {
            await _tagRepository.Create(dbRecord);
        }
        catch (Exception e)
        {
            return ResponseService<Tag>.Error(Errors.CANT_CREATE_TAG_ERROR);
        }
        
        return ResponseService<Tag>.Ok(dbRecord);
    }

    public async Task<ICollection<Tag>> GetAll(string userName)
    { 
        var user = await _manager.FindByNameAsync(userName);
        return await _tagRepository.GetAll()
            .Where(x => x.UserId == user.Id)
            .ToListAsync();
    }
}
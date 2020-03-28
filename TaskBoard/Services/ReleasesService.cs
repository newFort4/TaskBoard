using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Models;
using TaskBoard.ViewModels;
using TaskBoard.ViewModels.ReleasesModels;

namespace TaskBoard.Services
{
    public class ReleasesService
    {
        readonly TaskBoardDbContext db;
        readonly UserManager<IdentityUser> userManager;

        public ReleasesService(TaskBoardDbContext db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<EditReleaseModel> GetReleaseForEditAsync(int releaseId)
        {
            var release = await GetReleaseAsync(releaseId);

            return new EditReleaseModel
            {
                ReleaseId = release.ReleaseId,
                AssignedTo = release.AssignedTo,
                Title = release.Title,
                Description = release.Description,
                ReleaseDate = release.ReleaseDate
            };
        }

        public async Task<ReleaseDetailsModel> GetReleaseAsync(int releaseId)
        {
            var release = await db.Releases.FindAsync(releaseId);

            var releaseDetailsModel = ReleaseDetailsModel.ToControllerModel(release);

            return releaseDetailsModel;
        }

        public IQueryable<Release> GetReleases()
        {
            return db
                .Releases
                .Include(x => x.AssignedTo)
                .AsNoTracking();
        }

        public async Task<BaseResultsModel<Release>> GetReleasesAsync(ReleaseSearchModel releaseSearchModel = null)
        {
            var releases = GetReleases();

            if (releaseSearchModel != null)
            {
                if (!string.IsNullOrEmpty(releaseSearchModel.AssignedTo))
                {
                    if (releaseSearchModel.AssignedTo == "/Unassigned")
                    {
                        releases = releases.Where(x => x.AssignedTo == null);
                    }
                    else
                    {
                        releases = releases.Where(x => x.AssignedTo.Email.Contains(releaseSearchModel.AssignedTo));
                    }
                }

                if (!string.IsNullOrEmpty(releaseSearchModel.Title))
                {
                    releases = releases.Where(x => x.Title.Contains(releaseSearchModel.Title));
                }

                if (releaseSearchModel.ReleaseDate.HasValue)
                {
                    releases = releases.Where(x => x.ReleaseDate < DateTime.Now);
                }
            }

            return new BaseResultsModel<Release>(await releases.CountAsync(), await releases
                .OrderBy(x => x.ReleaseId)
                .Skip(releaseSearchModel.PageSize * (releaseSearchModel.CurrentPage - 1))
                .Take(releaseSearchModel.PageSize)
                .ToListAsync());
        }

        public async Task CreateReleaseAsync(CreateReleaseModel createReleaseModel)
        {
            var identityUser = !string.IsNullOrEmpty(createReleaseModel.AssignedTo) ?
                await userManager.FindByEmailAsync(createReleaseModel.AssignedTo) :
                null;

            var release = new Release
            {
                Title = createReleaseModel.Title,
                Description = createReleaseModel.Description,
                AssignedTo = identityUser,
                ReleaseDate = createReleaseModel.ReleaseDate
            };

            await db.Releases.AddAsync(release);
            await db.SaveChangesAsync();
        }

        public async Task EditReleaseAsync(EditReleaseModel editReleaseModel)
        {
            var release = await db.Releases.FindAsync(editReleaseModel.ReleaseId);

            var identityUser = await db.Users.SingleOrDefaultAsync(x => x.Email == editReleaseModel.AssignedTo);

            release.AssignedTo = identityUser;
            release.Title = editReleaseModel.Title;
            release.Description = editReleaseModel.Description;
            release.ReleaseDate = editReleaseModel.ReleaseDate;

            await db.SaveChangesAsync();
        }

        public async Task RemoveReleaseAsync(int releaseId)
        {
            db.Releases.Remove(await db.Releases.FindAsync(releaseId));
            await db.SaveChangesAsync();
        }
    }
}

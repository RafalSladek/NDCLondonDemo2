using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using NDCLondonDemo2.model;

namespace NDCLondonDemo2
{
    [Authorize]
    public class ArticlesController : ApiController
    {
        private readonly NDCWorkshopEntities _db;

        public ArticlesController()
        {
            _db = new NDCWorkshopEntities();
        }

        public ArticleDto Get(string id)
        {
            var currentGuid = new Guid(id);

            var result = from a in _db.Articles
                         where a.Id == currentGuid
                         select new ArticleDto
                         {
                             Id = currentGuid,
                             Name = a.Name,
                             Description = a.Description,
                             Category = a.Category.Name,
                             Code = a.Code,
                             ImageUrl = a.ImageUrl
                         };
            var articleDto = result.FirstOrDefault();

            if (articleDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return articleDto;
        }

        [Queryable]
        public IQueryable<ArticleListDto> GetAll()
        {
            var results = (from a in _db.Articles
                           select new ArticleListDto { Id = a.Id, Name = a.Name, Description = a.Description });

            return results;
        }

        public ArticleDto Post(ArticleDto articleDto)
        {
            var a = new Article
            {
                Name = articleDto.Name,
                Description = articleDto.Description,
                Code = articleDto.Code
            };

            if (articleDto.Id == Guid.Empty)
            {
                //Insert
                a.Id = Guid.NewGuid();
                a.CategoryId = Guid.Parse("F5C00A61-FFE8-4D8C-8F19-2B71908B1976");
                _db.Entry(a).State = EntityState.Added;
            }
            else
            {
                //Update
                a.Id = articleDto.Id;
                _db.Entry(a).State = EntityState.Modified;
                _db.Entry(a).Property(ar => ar.CategoryId).IsModified = false;
            }

            _db.SaveChanges();

            return new ArticleDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Code = a.Code,
                ImageUrl = a.ImageUrl
            };
        }

        public void Delete(string id)
        {
            var articleToBeDeleted = Get(id);
            var a = new Article
            {
                Id = articleToBeDeleted.Id
            };

            _db.Entry(a).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_db != null)
            {
                _db.Dispose();
            }
        }
    }
}
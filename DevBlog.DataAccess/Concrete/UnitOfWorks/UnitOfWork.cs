using DevBlog.Core.DataAccess.Abstract;
using DevBlog.DataAccess.Concrete.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.DataAccess.Concrete.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IArticleDal Articles { get; }
        public ICategoryDal Categories { get; }
        public ITagDal Tags { get; }
        public ICommentDal Comments { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Articles = new EfArticleDal(_context);
            Categories = new EfCategoryDal(_context);
            Tags = new EfTagDal(_context);
            Comments = new EfCommentDal(_context);
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

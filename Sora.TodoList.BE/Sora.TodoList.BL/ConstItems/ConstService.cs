using Microsoft.Extensions.DependencyInjection;
using Sora.TodoList.BL.ConstItems.Dtos;
using Sora.TodoList.DL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sora.TodoList.BL.ConstItems
{
    public interface IConstService
    {
        /// <summary>
        /// Lấy danh sách const
        /// </summary>
        /// <param name="constListKey"></param>
        /// <returns></returns>
        Task<List<ConstItemDto>> GetConstList(string constListKey);
    }

    public class ConstService : IConstService
    {
        #region Khởi tạo

        private readonly IConstRepository _constRepository;

        public ConstService(IServiceProvider serviceProvider)
        {
            _constRepository = serviceProvider.GetRequiredService<IConstRepository>();
        }

        #endregion Khởi tạo

        #region Hàm

        /// <summary>
        /// Lấy danh sách const
        /// </summary>
        /// <param name="constListKey"></param>
        /// <returns></returns>
        public async Task<List<ConstItemDto>> GetConstList(string constListKey)
        {
            var result = await _constRepository.GetConstList(constListKey);
            return [..result.Select(x => new ConstItemDto
            {
                Key = x.Key,
                Value = x.Value,
                ExtraProperties = x.ExtraProperties,
            })];
        }

        #endregion Hàm
    }
}
using BattleshipDL;
using BattleshipModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBL
{
    public class StatisticBL : IStatisticBL
    {
        private IStatisticDL _repo;

        public StatisticBL(IStatisticDL p_repo)
        {
            _repo = p_repo;
        }
        public async Task<Statistic> AddStatisticAsync(Statistic p_stat)
        {
            return await _repo.AddStatisticAsync(p_stat);
        }

        public async Task<List<Statistic>> GetAllStatisticsAsync()
        {
            return await _repo.GetAllStatisticsAsync();
        }

        public async Task<Statistic> GetStatisticAsync(int p_sId)
        {
            return await _repo.GetStatisticAsync(p_sId);
        }

        public async Task<Statistic> UpdateStatisticAsync(Statistic p_stat)
        {
            return await _repo.UpdateStatisticAsync(p_stat);
        }
    }
}

using BattleshipDL;
using BattleshipModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBL
{
    public class ScoreBL : IScoreBL
    {
        private IScoreDL _repo;

        public ScoreBL(IScoreDL p_repo)
        {
            _repo = p_repo;
        }
        public async Task<Score> AddScoreAsync(Score p_score)
        {
            return await _repo.AddScoreAsync(p_score);
        }

        public async Task<List<Score>> GetAllScoresAsync()
        {
            return await _repo.GetAllScoresAsync();
        }

        public async Task<Score> GetScoreAsync(int p_sId)
        {
            return await _repo.GetScoreAsync(p_sId);
        }

        public async Task<Score> UpdateScoreAsync(Score p_score)
        {
            return await _repo.UpdateScoreAsync(p_score);
        }
    }
}

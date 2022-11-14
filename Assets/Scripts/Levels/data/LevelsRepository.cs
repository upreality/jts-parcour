using System;
using System.Collections.Generic;
using System.Linq;
using Levels.data.model;
using Levels.domain.model;
using Levels.domain.repositories;
using UnityEngine;
using Zenject;
using static Levels.domain.repositories.ILevelsRepository;

namespace Levels.data
{
    public class LevelsRepository : ILevelsRepository, ILevelSceneObjectRepository
    {
        [Inject] private ILevelsDao levelsDao;

        public List<Level> GetLevels() => levelsDao.GetLevelEntities().Select(GetLevel).ToList();

        public Level GetLevel(long levelId)
        {
            var index = Convert.ToInt32(levelId);
            var entity = GetById(levelId);
            return GetLevel(entity, index);
        }
        
        public GameObject GetLevelScene(long levelId) => levelId == HubId ? 
            levelsDao.GetHub() 
            : GetById(levelId).scenePrefab;

        private Level GetLevel(LevelEntity entity, int index)
        {
            var id = Convert.ToInt64(index);
            return new Level(id, index + 1, entity.reward);
        }

        private LevelEntity GetById(long levelId)
        {
            var index = Convert.ToInt32(levelId);
            return levelsDao.GetLevelEntities()[index];
        }

        public interface ILevelsDao
        {
            public List<LevelEntity> GetLevelEntities();
            public GameObject GetHub();
        }
    }
}
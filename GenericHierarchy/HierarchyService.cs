﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericHierarchy
{
    public static class HierarchyService<T> where T : IBaseHierarchyEntity
    {
        public static List<HierarchyResult> GetHierarchyResults(List<T> entities)
        {
            var result = new List<HierarchyResult>();
            void AddHierarchyToResults(T entity, int degree, List<int> parents)
            {
                var currentParents = new List<int>(parents);
                currentParents.Add(entity.Id);
                result.Add(new HierarchyResult
                {
                    Id = result.Count + 1,
                    EntityId = entity.Id,
                    Name = entity.Name,
                    ParentEntityId = entity.ParentId,
                    Degree = degree,
                    Parents = currentParents
                });
                var childEntites = entities.Where(o => o.ParentId == entity.Id);
                foreach (var child in entities.Where(o => o.ParentId == entity.Id))
                {
                    AddHierarchyToResults(child, degree + 1, currentParents);
                }
            }
            var rootEntities = entities.Where(o => o.ParentId == null);
            foreach (var rootEntity in entities.Where(o => o.ParentId == null))
            {
                AddHierarchyToResults(rootEntity, 0, new List<int>());
            }
            foreach (var entityResult in result)
            {
                var subEntities = result
                    .Where(o => o.Parents.Contains(entityResult.EntityId))
                    .Select(o => o.EntityId).ToList();
                subEntities.Sort();
                entityResult.SubEntities = subEntities;
            }
            return result;
        }


    }
    public interface IBaseHierarchyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
    public class HierarchyResult
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public int? ParentEntityId { get; set; }
        public int Degree { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<int> Parents { get; set; } = new List<int>();
        public List<int> SubEntities { get; set; } = new List<int>();
    }
}

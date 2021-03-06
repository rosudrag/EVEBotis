﻿using System;
using EVE.ISXEVE;
using GalaSoft.MvvmLight;

namespace ILoveEVE.Core.Model
{
  public class EntityViewModel : ObservableObject, IComparable<EntityViewModel>
  {
    /// <summary>
    ///   The entity
    /// </summary>
    private Entity entity;

    /// <summary>
    ///   Gets or sets the entity.
    /// </summary>
    /// <value>
    ///   The entity.
    /// </value>
    public Entity Entity
    {
      get { return entity; }
      set { entity = value; }
    }

    /// <summary>
    ///   Gets the entity identifier.
    /// </summary>
    /// <value>
    ///   The entity identifier.
    /// </value>
    public string EntityId
    {
      get { return Convert.ToString(entity.ID); }
    }

    /// <summary>
    ///   Gets or sets the name of the entity.
    /// </summary>
    /// <value>
    ///   The name of the entity.
    /// </value>
    public string EntityName
    {
      get { return Entity.Name; }
    }

    public string EntityGroup
    {
      get { return entity.Group; }
    }

    public string EntityCloaked
    {
      get { return entity.IsCloaked ? "Cloaked" : "NotCloaked"; }
    }

    public double EntityDistanceTo
    {
      get { return entity.Distance; }
    }

    public int EntityStandings { get; set; }

    public int CompareTo(EntityViewModel other)
    {
      if (EntityGroup.ToLower().Equals("capsule"))
      {
        return -2;
      }

      return string.Compare(EntityGroup, other.EntityGroup, StringComparison.Ordinal);
    }

    public override string ToString()
    {
      return string.Format("{{Name: {0}, Standings: {1}, DistanceTo: {2}, Id: {3}}}", EntityName, EntityStandings,
        EntityDistanceTo, EntityId);
    }
  }
}
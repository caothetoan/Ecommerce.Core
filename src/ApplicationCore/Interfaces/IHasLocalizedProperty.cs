using System;
using System.Collections.Generic;
using System.Text;
using Vnit.ApplicationCore.Entities;

namespace Vnit.ApplicationCore.Interfaces
{
    public interface IHasLocalizedProperty<T> : IHasLocalizedProperty where T : BaseEntity
    {

    }

    public interface IHasLocalizedProperty
    {
        int Id { get; set; }
    }
}

﻿using MappingGenerator.Enums;
using System.Collections.Generic;

namespace MappingGenerator.Models;

internal sealed class PropertyDeclaration
{
    public PropertyDeclaration(string name, PropertyBaseType baseType, PropertyModifierType modifierType)
    {
        Name = name;
        BaseType = baseType;
        ModifierType = modifierType;
    }

    public string Name { get; private set; }
    public PropertyBaseType BaseType { get; private set; }
    public PropertyModifierType ModifierType { get; private set; }

    public override bool Equals(object obj)
    {
        // validate obj type
        if (obj is not PropertyDeclaration property) return false;

        // TODO: Isso deve e vai ser melhorado... por em quanto isso server
        if(
            property.Name == Name && 
            property.ModifierType == ModifierType &&
            property.BaseType == BaseType) return true;

        return false;
    }

    public override int GetHashCode()
    {
        int hashCode = -1477777586;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        hashCode = hashCode * -1521134295 + BaseType.GetHashCode();
        hashCode = hashCode * -1521134295 + ModifierType.GetHashCode();
        return hashCode;
    }
}

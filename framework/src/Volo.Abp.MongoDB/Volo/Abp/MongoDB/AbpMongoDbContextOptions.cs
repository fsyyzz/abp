﻿using System;
using System.Collections.Generic;

namespace Volo.Abp.MongoDB
{
    public class AbpMongoDbContextOptions
    {
        internal Dictionary<Type, Type> DbContextReplacements { get; }

        public AbpMongoDbContextOptions()
        {
            DbContextReplacements = new Dictionary<Type, Type>();
        }
        
        internal Type GetReplacedTypeOrSelf(Type dbContextType)
        {
            var replacementType = dbContextType;
            while (true)
            {
                if (DbContextReplacements.TryGetValue(replacementType, out var foundType))
                {
                    if (foundType == dbContextType)
                    {
                        throw new AbpException(
                            "Circular DbContext replacement found for " +
                            dbContextType.AssemblyQualifiedName
                        );
                    }
                    
                    replacementType = foundType;
                }
                else
                {
                    return replacementType;
                }
            }
        }
    }
}
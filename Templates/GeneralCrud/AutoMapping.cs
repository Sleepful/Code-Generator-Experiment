                CreateMap<{!FIELD}, GeneralModel>()
                   .ForMember(m => m.EntityId, obj => obj.MapFrom(e => e.{!FIELD}Id))
                   .ForMember(m => m.Name, obj => obj.MapFrom(e => e.{!FIELD}Name))
                   .ReverseMap();
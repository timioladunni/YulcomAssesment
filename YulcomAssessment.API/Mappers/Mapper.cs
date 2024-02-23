using YulcomAssesment.API.Data;
using YulcomAssesment.API.Models.Yulcom;
using YulcomAssesment.API.Response;

namespace YulcomAssesment.API.Mappers
{
    public static class Mapper
    {
        public static YulcomAssesmentData AsDto(this YulcomModel model)
        {
            return new YulcomAssesmentData
            {
                Attack = model.Attack,
                Defense = model.Defense,
                SpDefense = model.SpDefense,
                Generation = model.Generation,
                SpAttack = model.SpAttack,
                Speed = model.Speed,
                HP = model.HP,
                Legendary = model.Legendary,
                Name = model.Name,
                Total = model.Total,
                Type1 = model.Type1,
                Type2   = model.Type2
            };
        }

        public static YulcomModel AsDto(this YulcomAssesmentData model)
        {
            return new YulcomModel
            {
                Attack = model.Attack,
                Defense = model.Defense,
                SpDefense = model.SpDefense,
                Generation = model.Generation,
                SpAttack = model.SpAttack,
                Speed = model.Speed,
                HP = model.HP,
                Legendary = model.Legendary,
                Name = model.Name,
                Total = model.Total,
                Type1 = model.Type1,
                Type2 = model.Type2
            };
        }

        public static List<YulcomModel> AsDto(this  List<YulcomAssesmentData> model) 
        {
            return model.Select(model => new YulcomModel
            {
                Attack = model.Attack,
                Defense = model.Defense,
                SpDefense = model.SpDefense,
                Generation = model.Generation,
                SpAttack = model.SpAttack,
                Speed = model.Speed,
                HP = model.HP,
                Legendary = model.Legendary,
                Name = model.Name,
                Total = model.Total,
                Type1 = model.Type1,
                Type2 = model.Type2

            }).ToList();
        }

        public static List<YulcomAssesmentData> AsDto(this List<YulcomModel> model)
        {
            return model.Select(model => new YulcomAssesmentData
            {
                Attack = model.Attack,
                Defense = model.Defense,
                SpDefense = model.SpDefense,
                Generation = model.Generation,
                SpAttack = model.SpAttack,
                Speed = model.Speed,
                HP = model.HP,
                Legendary = model.Legendary,
                Name = model.Name,
                Total = model.Total,
                Type1 = model.Type1,
                Type2 = model.Type2

            }).ToList();
        }
    }
}

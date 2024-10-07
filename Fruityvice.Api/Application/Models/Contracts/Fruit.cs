namespace Fruityvice.Api.Application.Models.Contracts;

public record Fruit(
   string Name,
   int Id,
   string Family,
   string Order,
   string Genus,
   Nutritions Nutritions
);

public record Nutritions(
    int Calories,
    double Fat,
    double Sugar,
    double Carbohydrates,
    double Protein
);

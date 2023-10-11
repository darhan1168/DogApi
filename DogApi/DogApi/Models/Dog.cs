using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogApi.Models;

//name color tail_length weight


public class Dog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("color")]
    public string Color { get; set; }

    [Column("tail_length")]
    public decimal TailLength { get; set; }

    [Column("weight")]
    public decimal Weight { get; set; }
}
using System;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id {get; set;}
    public string Name {get;set;}
    [Required]
    public string Email {get;set;}
    [Required]
    public string Password{get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt{get;set;}
}
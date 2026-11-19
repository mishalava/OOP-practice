using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSport.Data;

public class Workout
{
    [Key]
    public int Id { get; set; }
    public string FileName { get; set; }
    public DateTime Time { get; set; }
    public int Distance { get; set; }


}

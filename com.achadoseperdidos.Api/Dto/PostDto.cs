using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using com.achadoseperdidos.Api.Data;
using com.achadoseperdidos.Api.Validations;

namespace com.achadoseperdidos.Api.DTO;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public string? ItemImageUrl { get; set; }
    public string? ItemColor { get; set; }
    public string ItemFoundLocation { get; set; }
    public string? ItemFoundReference { get; set; }
    public string ItemCity { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
    public DateOnly? ItemDateFound { get; set; }
}

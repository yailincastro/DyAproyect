
using DyAproyect.Data.Constant;

namespace DyAproyect.Data.Resquest
{

    public class ImagenRequest
{
    public string DatosBase64 { get; set; } = Imagenes.DefaultFile;
    public string ToDisplayIMG => $"data:image/png;base64,{DatosBase64}";
}

}
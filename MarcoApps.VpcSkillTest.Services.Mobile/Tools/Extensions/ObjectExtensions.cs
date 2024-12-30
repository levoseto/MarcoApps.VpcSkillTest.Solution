namespace MarcoApps.VpcSkillTest.Services.Mobile.Tools.Extensions
{
    using System.Text.Json;

    public static class ObjectExtensions
    {
        /// <summary>
        /// Serializa un objeto a JSON con formato legible para depuración.
        /// </summary>
        /// <param name="obj">El objeto a serializar.</param>
        /// <returns>Una cadena JSON representando el objeto.</returns>
        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            return JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true // Formato legible
            });
        }
    }
}
namespace Laberinto.Core.Models
{
    /// Representa un armario en el laberinto, un tipo de contenedor especializado

    public class Armario : Contenedor
    {
        /// Indica que este elemento es un armario. (Smalltalk esArmario) </summary>
        public override bool EsArmario => true;  

        /// Acepta un visitante específico para armarios. (Smalltalk visitarContenedor:) </summary>
        public override void Accept(IVisitor visitor)  
        {
            visitor.VisitArmario(this);
        }
    }
}

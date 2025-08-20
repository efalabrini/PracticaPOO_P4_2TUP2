namespace PracticaPOO_P4_2TUP2.Core.Entities;

// creacion de clase para la solicitud de creacion de cuenta bancaria por medio de FromBody
// se usa para recibir los datos de la solicitud HTTP POST
// y se mapea a la clase CreateBankAccountReq
// que contiene las propiedades Owner e InitialBalance
public class CreateBankAccountReq
{
    public string Owner { get; set; }
    public decimal InitialBalance { get; set; }
}
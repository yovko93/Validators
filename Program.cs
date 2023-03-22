using Validators.Email_Validator;
using Validators.Phone_Validator;

var validator = new EmailValidator();

while (true)
{
    //Console.WriteLine("Insert PhoneNumber: (889 754650 or xxx):");
    Console.WriteLine("Insert email: ");
    string? input = Console.ReadLine();

    try
    {
        bool phoneInfo = validator.IsValid(input);
        Console.WriteLine(phoneInfo);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
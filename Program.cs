using Validators.Phone_Validator;

var validator = new PhoneValidator();

while (true)
{
    Console.WriteLine("Insert PhoneNumber: (889 754650 or xxx):");
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
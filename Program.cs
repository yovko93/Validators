using Validators.PhoneValidator;

var validator = new PhoneValidator();

while (true)
{
    Console.WriteLine("Insert PhoneNumber: (xxx or xxx):");
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
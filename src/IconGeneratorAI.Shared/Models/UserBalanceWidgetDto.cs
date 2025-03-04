namespace IconGeneratorAI.Shared.Models;

public sealed record UserBalanceWidgetDto
{
    public int Balance { get; set; }

    public UserBalanceWidgetDto(int balance)
    {
        Balance = balance;
    }

    public UserBalanceWidgetDto()
    {

    }
}

namespace Domain.Enum;

public enum PostStatus : byte
{
    WaitingApproval = 0,
    Approved = 1,
    Refused = 2,
    Returned = 3
}
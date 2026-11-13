namespace LOK2239;

// no remarks: all good, we are before letter 1
// letter pending: send letter as soon as possible
// letter sent: letter was sent and we are not awaiting what to do next
public enum StudentState 
{
    NoRemarks,      // all good
    Letter1Pending, // send letter1 as soon as possible
    Letter1Sent,    // letter1 was sent
    Letter2Pending,
    Letter2Sent,
    Letter3Pending,
    Letter3Sent,
};

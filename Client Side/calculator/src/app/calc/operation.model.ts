export interface history{
    last3OfType: operation[],
    lastMonthOfType: operation[]
}

export interface operation{
    number1:number,
    number2:number,
    operationType:string,
    result:number
}
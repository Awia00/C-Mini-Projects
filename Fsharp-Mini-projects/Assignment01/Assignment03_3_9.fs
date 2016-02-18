module Assignment03_3_9
exception DivideByZero
type complexNumber = V of float * float
let (.+.) (V(a,b)) (V(c,d)) = V(a+c,b+d)
let (.*.) (V(a,b)) (V(c,d)) = V(a*c - b*d, b*c + a*d)
let (~-) (V(a,b)) = V(-a,-b)
let (.-.) v1 v2 = v1 .+. -v2
let inverseMulti (V(a,b)) = if (a=0.0 || b=0.0) then raise DivideByZero else 
                                        let add = a*a+b*b
                                        V(a/add, (b*(-1.0))/add)
let (./.) a b = a .*. (inverseMulti b)
let make (x,y)= V(x,y)
let unMake (V(x,y)) = (x,y)
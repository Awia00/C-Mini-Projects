module Assignment03_3_9
type complexNumber
val (.+.) : complexNumber -> complexNumber -> complexNumber 
val (.*.) : complexNumber -> complexNumber -> complexNumber 
val (~-) : complexNumber -> complexNumber 
val (.-.) : complexNumber -> complexNumber -> complexNumber 
val inverseMulti : complexNumber -> complexNumber 
val (./.) : complexNumber -> complexNumber -> complexNumber
val make : float * float -> complexNumber
val unMake : complexNumber -> float * float
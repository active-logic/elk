namespace Elk.Basic{
    
// When intercepting a function call, used to return information
// for further processing:
// e: set to indicate arguments have been evaluated
//    (runner should skip evaluating arguments)
// i: set to indicate a successful intercept
//    (in which case, runner will not evaluate the function)
// r: a return value provided as substitute (if intercepted)
public readonly struct Pass{

    // did evaluate arguments (don't re-eval)
    readonly public bool   e;
    // did intercept (don't evaluate function)
    readonly public bool   i;
    // the return value (if intercepted)
    readonly public object r;

    public Pass(bool i, bool e, object r){
        this.i = i; this.e = e; this.r = r;
    }

}}

#include <iostream>
using namespace std;
 
int main() {
    int x, y, z, Max;
 
    x = 1;
	y = 2;
	z = 3;
    
    cout << "Способ №1" << endl;
    if(x > y) {
        if(x > z) Max = x;
        else Max = z;
    }
    else {
        if(y > z) Max = y;
        else Max = z;
    }
    cout << "Самое большое число - " << Max << endl;
    
    cout << "Способ №2" << endl;
    if(x > y && x > z) Max = x;
    if(y > x && y > z) Max = y;
    else Max = z; 
    cout << "Самое большое число - " << Max << endl; 
 
 
    cout << "Способ №3" << endl;
    if(x > y) Max = x;
    else Max = y;
    if(Max < z) Max = z;
    cout << "Самое большое число - " << Max << endl;
 
 
    cout << "Способ №4" << endl;
    Max = (x > y)? x : y;
    Max = (Max > z)? Max : z; 
    cout << "Самое большое число - " << Max << endl;
 
 
    system('pause');
    return 0;
}
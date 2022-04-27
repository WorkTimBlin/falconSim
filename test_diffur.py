import numpy as np
from decimal import *
import matplotlib.pyplot as plot
from progress.bar import IncrementalBar

x = np.arange(0, 5, 0.001)

class pieceLinear_func:
    def __init__(self, a, b, number_of_nodes):
        self.a = a
        self.b = b
        self.h = Decimal((b - a) / number_of_nodes)
        self.number_of_nodes = number_of_nodes
        self.nodes = np.zeros(number_of_nodes, dtype=Decimal)
    #just the array of values of the function

    def set_node(self, i, value):
        self.nodes[i] = value
    
    #so, the derivative at i would be (node[i + 1] - node[i]) / h, keeping the formula for higher orders
    def derivative(self, i, order):
        if(order == 0):
            return self.nodes[i]
        else:
            return (self.derivative(i + 1, order - 1) - self.derivative(i, order - 1)) / self.h
        #not the perfect solution with recurtion, but alright to run once

    #this function set the node to a specific value to get the 
    # given dirivative of given order at given node exactly the given value
    def set_derivative_based_on_prev(self, i, order, value):
        if(order == 0):
            self.nodes[i] = Decimal(value)
        else:
            self.set_derivative_based_on_prev(i + 1, order - 1, self.derivative(i, order - 1) + value * self.h)
        #even worse solution with combined recurtion
        #the solution would be to store the known derivatives dynamically along with the nodes, but it
        #takes 5 times more memory and needs a good restiction functionality, so i thought that isn't worth it
        #for this single task


#creating the func
y = pieceLinear_func(0, 5, 5000)

#d^5(y) + 15 d^4(y) + 90 d^3(y) + 270 d^2(y) + 405 dy + 243 y = 0 (*)
#gets us the d^5(y), given lower order derivatives:
def fifth_derivative(y:pieceLinear_func, i):
    return -15 * pieceLinear_func.derivative(y, i, 4) - 90 * pieceLinear_func.derivative(y, i, 3) - 270 * pieceLinear_func.derivative(y, i, 2) - 405 * pieceLinear_func.derivative(y, i, 1) - 243 * pieceLinear_func.derivative(y, i, 0)


#y(0) = 0, dy(0) = 3, d^2(y)(0) = -9, d^3(y)(0) = -8, d^4(y)(0) = 0
#setting up 0
y.set_derivative_based_on_prev(0, 0, 0)
y.set_derivative_based_on_prev(0, 1, 3)
y.set_derivative_based_on_prev(0, 2, -9)
y.set_derivative_based_on_prev(0, 3, -8)
y.set_derivative_based_on_prev(0, 4, 0)


#print(str(y.derivative(0, 0)) + ' ' + str(y.derivative(1, 0))  + ' ' + str(y.derivative(2, 0))  + ' ' + str(y.derivative(3, 0))  + ' ' + str(y.derivative(4, 0))  + ' ')

#print(str(y.derivative(0, 0)) + ' ' + str(y.derivative(0, 1))  + ' ' + str(y.derivative(0, 2))  + ' ' + str(y.derivative(0, 3))  + ' ' + str(y.derivative(0, 4))  + ' ')

#as the derivateves at i up to 4th give us the 5th, 
# and the 5th sets up derivatives up to 4th at i+1, 
#we can now fill the rest of y:

bar = IncrementalBar('building y...', max=y.number_of_nodes - 5)
for i in range(0, y.number_of_nodes - 5):
    y.set_derivative_based_on_prev(i, 5, fifth_derivative(y, i))
    bar.next()
bar.finish()

plot.plot(np.arange(0, 5, y.h) ,y.nodes)
plot.show()

#Shabalin Timofey Andreevich, timofeusPharaon@gmail.com, +79173581946
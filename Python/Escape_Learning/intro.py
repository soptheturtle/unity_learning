import datetime
WIDTH = 300
HEIGHT = 300
i = 0
def draw():
	global i
	i+=1
	print("{0} at {1}".format(i,datetime.datetime.now()))
	screen.fill((200,150,200))


clock.schedule_interval(draw,1)
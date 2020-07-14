import time, random, math

class joe_ball():
	print('Initializing a joe_ball!')
	global WIDTH, HEIGHT, PADDLE_WIDTH, PADDLE_HEIGHT
	global x_paddle, y_paddle, FIRST_LOOP, difficulty, score
	import pygame
	def __init__(self, ball_x, ball_y, image):
		self.score = 0
		self.image = image
		self.ball_x_old = ball_x
		self.ball_y_old = ball_y
		self.ball_x = ball_x
		self.ball_y = ball_y
		self.BALL_WIDTH = self.image.get_width()
		self.up_flag = random.randint(0,1)
		self.x_incr = random.randint(-11,11)
		self.ball_sound = 0
		while self.x_incr in (-10,-5,0,5,10): #Doesn't allow for straight back and forth bouncing of hte ball
			self.x_incr = random.randint(-11,11)

	def reset(self, ball_x, ball_y, score):
		self.score = score
		self.ball_x_old = ball_x
		self.ball_y_old = ball_y
		self.ball_x = ball_x
		self.ball_y = ball_y
		self.up_flag = random.randint(0,1)
		self.x_incr = random.randint(-10,10)
		self.ball_sound = 0
		while self.x_incr in (-10,-5,0,5,10): #Doesn't allow for straight back and forth bouncing of hte ball
			self.x_incr = random.randint(-11,11)		
	
	def move_the_ball(self, WIDTH, x_paddle, y_paddle, PADDLE_WIDTH, keyboard, difficulty):
		global score
		#score = score
		if self.ball_x > (WIDTH - 20): #Ball off right edge of screen
			self.ball_x = (WIDTH -20)
			self.x_incr = self.x_incr * (-1) # bounces the ball back
		elif self.ball_x < 0: #Ball off left edge of screen
			self.ball_x = 0
			self.x_incr = self.x_incr * (-1)  # bounces the ball back
		else: #normal motion
			self.ball_x += self.x_incr

		if self.up_flag == 1:
			if self.ball_y < 35: #Bounces the ball back if we get to the top wall
				self.score +=1
				self.up_flag = 0
				self.ball_y += difficulty
				self.ball_sound = 1
			else:
				self.ball_y -= difficulty
		elif self.up_flag == 0: #begin checks to see if the paddle is able to bounce the ball back
			if (self.ball_y < y_paddle) \
				and (self.ball_y > y_paddle - 20) \
				and (self.ball_x + self.BALL_WIDTH >= x_paddle) \
				and (self.ball_x <= x_paddle + PADDLE_WIDTH):
				self.up_flag = 1
				self.ball_y -= difficulty
				self.ball_sound = 2
				if keyboard.left: #give the ball more left velocity if paddle is moving
					self.x_incr -= 5
				if keyboard.right: #give the ball more right velocity if the paddle is moving
					self.x_incr += 5
			elif (self.ball_y < y_paddle) \
			      and (self.ball_y > y_paddle - 19) \
			      and ((self.ball_x + self.BALL_WIDTH in range(x_paddle-5,x_paddle+1)) or (self.ball_x in range(x_paddle + PADDLE_WIDTH, x_paddle + PADDLE_WIDTH+1))): #Checking if the ball hit side of paddle
				self.ball_sound = 2
				self.x_incr = self.x_incr * (-1)


			else:
				self.ball_y += difficulty # ball continues to move as normal


class joe_paddle():
	print('Initializing a joe_paddle!')
	global WIDTH, HEIGHT
	global x_paddle, y_paddle, FIRST_LOOP

	def __init__(self, WIDTH, HEIGHT, image):
		self.image = image
		self.y_paddle = int(HEIGHT*(7/8))
		self.x_paddle = int(WIDTH/2) - int(self.image.get_width()/2)
		self.PADDLE_HEIGHT = self.image.get_height()
		self.PADDLE_WIDTH = self.image.get_width()
		

	def move_the_paddle(self, keyboard, WIDTH):
		#Moving the paddle back and forth
		if keyboard.right and self.x_paddle <= WIDTH - self.PADDLE_WIDTH:
			self.x_paddle = self.x_paddle + 6
		if keyboard.left and self.x_paddle >= 0:
			self.x_paddle = self.x_paddle - 6
		#If the player somehow goes out of bounds put the paddle back on the screen
		if self.x_paddle > WIDTH - self.PADDLE_WIDTH:
			self.x_paddle = WIDTH - self.PADDLE_WIDTH
		if self.x_paddle < 0:
			self.x_paddle = 0


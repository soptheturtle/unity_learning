#======================================================
#Joe Pong
#By Joseph Garland, likely released June 21st, 2020
#======================================================

import time, random, math
import pygame
from joe_classes import joe_ball, joe_paddle

###############
## VARIABLES ##
###############


WIDTH = 800 # Window Size
HEIGHT = 800


BACKGROUND_BOX  = Rect((0, 0), (800, 800))
TOP_BOX = Rect((0,0), (800, 25))
GREEN = (0,255,0)
BLACK = (0,0,0)
WHITE = (255,255,255)

BALL_START = (400,200)
BALL_START_VELOCITY = (5,5)
FIRST_LOOP = True

#Various scene states
menu_position = 0
option_position = 0
in_menu = True
in_game = False
in_options = False

#Options the player can control
LIVES_VECTOR = [1,3,5,10]
lives_vector_position = 1
lives = LIVES_VECTOR[lives_vector_position]

DIFFICULTY_VECTOR =[5,10,15]
difficulty_vector_position = 1
difficulty = DIFFICULTY_VECTOR[difficulty_vector_position]


TARGET_VECTOR = [10,15,20,100]
target_vector_position = 0
target_score = TARGET_VECTOR[target_vector_position]
score = 0

game_over = False
from_menu_first = True

####################
## DRAW FUNCTIONS ##
####################

def draw_image(image, x, y): #1
	screen.blit(image, (x,y))

def draw_rectangle(rectangle, color):
	screen.draw.filled_rect(rectangle, color)

########################
## INITIALIZE OBJECTS ##
########################
ball = joe_ball(BALL_START[0], BALL_START[1], images.ball)
paddle = joe_paddle(WIDTH, HEIGHT, images.paddle)

#####################
## SET UP THE MENU ##
#####################

###############
## GAME LOOP ##
###############


def menu_loop():
	global menu_position, in_menu, in_game, in_options, score, LIVES_VECTOR, lives_vector_position, from_menu_first
	draw_rectangle(BACKGROUND_BOX,BLACK)
	draw_image(images.menu_pointer, int(WIDTH/3), int(HEIGHT*((menu_position+7)/15)))
	draw_image(images.joetitle, int(WIDTH/2) -int(images.joetitle.get_width()/2), int(HEIGHT * 0.1))
	
	#Set score to 0
	score = 0
	lives = LIVES_VECTOR[lives_vector_position]
	from_menu_first = True
	#Draw title screen options
	draw_image(images.start_game, int(WIDTH/3) + int(WIDTH/35), int(HEIGHT*((0+7)/15)))
	draw_image(images.options, int(WIDTH/3) + int(WIDTH/35), int(HEIGHT*((1+7)/15)))
	draw_image(images.quit_game, int(WIDTH/3) + int(WIDTH/28), int(HEIGHT*((2+7)/15)))

	#Draw title screen instructions
	screen.draw.text("Use arrow keys to move.", (int(WIDTH/20), int(HEIGHT*(18/20))), color = WHITE)
	screen.draw.text("Use spacebar to make a selection.", (int(WIDTH/20), int(HEIGHT*(18.5/20))), color = WHITE)

	if keyboard.down:
		menu_position += 1
		sounds.menu_select.play()
		time.sleep(0.25)
		if menu_position > 2:
			menu_position = 0
	if keyboard.up: 
		menu_position -= 1
		time.sleep(0.25)
		sounds.menu_select.play()
		if menu_position < 0:
			menu_position = 2

	if (keyboard.kp_enter or keyboard.space) and menu_position == 0:
		in_menu = False
		in_game = True
		clock.unschedule(menu_loop)
		clock.schedule_interval(game_loop, 1/60)

	elif (keyboard.kp_enter or keyboard.space) and menu_position == 1:
		in_menu = False
		in_options = True
		clock.unschedule(menu_loop)
		clock.schedule_interval(options_loop, 1/60)
	elif (keyboard.kp_enter or keyboard.space) and menu_position == 2:
		quit()


def options_loop():
	global menu_position, in_menu, in_game, in_options, option_position, BACKGROUND_BOX, BLACK, WIDTH, HEIGHT
	global lives, LIVES_VECTOR, lives_vector_position, DIFFICULTY_VECTOR, difficulty_vector_position, difficulty
	global target_score, target_vector_position, TARGET_VECTOR


	#Draw the basics

	draw_rectangle(BACKGROUND_BOX,BLACK)
	draw_image(images.menu_pointer, int(WIDTH/3), int(HEIGHT*((option_position+10)/20)))
	draw_image(images.joetitle, int(WIDTH/2) -int(images.joetitle.get_width()/2), int(HEIGHT * 0.1))

	#Draw title screen instructions
	screen.draw.text("Use arrow keys to move.", (int(WIDTH/20), int(HEIGHT*(18/20))), color = WHITE)
	screen.draw.text("Use spacebar to make a selection.", (int(WIDTH/20), int(HEIGHT*(18.5/20))), color = WHITE)

	#Set lives options
	lives = LIVES_VECTOR[lives_vector_position]
	screen.draw.text("Lives:", (int(WIDTH/3 + WIDTH/30), int(HEIGHT*((0+10)/20))), color = WHITE)
	if lives == LIVES_VECTOR[0]:
		screen.draw.text("1", (int(WIDTH/3 + WIDTH/8), int(HEIGHT*((0+10)/20))), color = GREEN)
	else: 
		screen.draw.text("1", (int(WIDTH/3 + WIDTH/8), int(HEIGHT*((0+10)/20))), color = WHITE)
	if lives == LIVES_VECTOR[1]:
		screen.draw.text("3", (int(WIDTH/3 + WIDTH/((80/100)*8)), int(HEIGHT*((0+10)/20))), color = GREEN)
	else:
		screen.draw.text("3", (int(WIDTH/3 + WIDTH/((80/100)*8)), int(HEIGHT*((0+10)/20))), color = WHITE)
	if lives == LIVES_VECTOR[2]:
		screen.draw.text("5", (int(WIDTH/3 + WIDTH/((65/100)*8)), int(HEIGHT*((0+10)/20))), color = GREEN)
	else:
		screen.draw.text("5", (int(WIDTH/3 + WIDTH/((65/100)*8)), int(HEIGHT*((0+10)/20))), color = WHITE)
	if lives == LIVES_VECTOR[3]:
		screen.draw.text("10", (int(WIDTH/3 + WIDTH/((55/100)*8)), int(HEIGHT*((0+10)/20))), color = GREEN)
	else:
		screen.draw.text("10", (int(WIDTH/3 + WIDTH/((55/100)*8)), int(HEIGHT*((0+10)/20))), color = WHITE)

	#Set up the number of lives
	if option_position == 0 and (keyboard.left or keyboard.right):
		if keyboard.left and lives_vector_position == 0:
			lives_vector_position = len(LIVES_VECTOR) - 1
		elif keyboard.right and lives_vector_position == len(LIVES_VECTOR) -1:
			lives_vector_position = 0
		elif keyboard.left:
			lives_vector_position -= 1
		elif keyboard.right:
			lives_vector_position += 1


	#Draw Difficulty options
	difficulty = DIFFICULTY_VECTOR[difficulty_vector_position]
	screen.draw.text("Difficulty:", (int(WIDTH/3 + WIDTH/30), int(HEIGHT*((1+10)/20))), color = WHITE)
	if difficulty == DIFFICULTY_VECTOR[0]:
		screen.draw.text("Easy", (int(WIDTH*(50/100)), int(HEIGHT*((1+10)/20))), color = GREEN)
	else:
		screen.draw.text("Easy", (int(WIDTH*(50/100)), int(HEIGHT*((1+10)/20))), color = WHITE)
	if difficulty == DIFFICULTY_VECTOR[1]:
		screen.draw.text("Medium", (int(WIDTH*(58/100)), int(HEIGHT*((1+10)/20))), color = GREEN)
	else:
		screen.draw.text("Medium", (int(WIDTH*(58/100)), int(HEIGHT*((1+10)/20))), color = WHITE)
	if difficulty == DIFFICULTY_VECTOR[2]:
		screen.draw.text("Hard", (int(WIDTH*(70/100)), int(HEIGHT*((1+10)/20))), color = GREEN)
	else:
		screen.draw.text("Hard", (int(WIDTH*(70/100)), int(HEIGHT*((1+10)/20))), color = WHITE)

	#Set up the difficulty
	if option_position == 1 and (keyboard.left or keyboard.right):
		if keyboard.left and difficulty_vector_position == 0:
			difficulty_vector_position = len(DIFFICULTY_VECTOR) - 1
		elif keyboard.right and difficulty_vector_position == len(DIFFICULTY_VECTOR) -1:
			difficulty_vector_position = 0
		elif keyboard.left:
			difficulty_vector_position -= 1
		elif keyboard.right:
			difficulty_vector_position += 1


	#Draw Score to Hit options
	target_score = TARGET_VECTOR[target_vector_position]
	screen.draw.text("Target Score:", (int(WIDTH/3 + WIDTH/30), int(HEIGHT*((2+10)/20))), color = WHITE)
	if target_score == TARGET_VECTOR[0]:
		screen.draw.text("10", (int(WIDTH*(53/100)), int(HEIGHT*((2+10)/20))), color = GREEN)
	else:
		screen.draw.text("10", (int(WIDTH*(53/100)), int(HEIGHT*((2+10)/20))), color = WHITE)
	if target_score == TARGET_VECTOR[1]:
		screen.draw.text("15", (int(WIDTH*(58/100)), int(HEIGHT*((2+10)/20))), color = GREEN)
	else:
		screen.draw.text("15", (int(WIDTH*(58/100)), int(HEIGHT*((2+10)/20))), color = WHITE)
	if target_score == TARGET_VECTOR[2]:
		screen.draw.text("20", (int(WIDTH*(63/100)), int(HEIGHT*((2+10)/20))), color = GREEN)
	else:
		screen.draw.text("20", (int(WIDTH*(63/100)), int(HEIGHT*((2+10)/20))), color = WHITE)
	if target_score == TARGET_VECTOR[3]:
		screen.draw.text("100", (int(WIDTH*(68/100)), int(HEIGHT*((2+10)/20))), color = GREEN)
	else:
		screen.draw.text("100", (int(WIDTH*(68/100)), int(HEIGHT*((2+10)/20))), color = WHITE)

	#Set up the target score
	if option_position == 2 and (keyboard.left or keyboard.right):
		if keyboard.left and difficulty_vector_position == 0:
			target_vector_position = len(TARGET_VECTOR) - 1
		elif keyboard.right and target_vector_position == len(TARGET_VECTOR) -1:
			target_vector_position = 0
		elif keyboard.left:
			target_vector_position -= 1
		elif keyboard.right:
			target_vector_position += 1



	#Draw Return
	screen.draw.text("Return to Title", (int(WIDTH/3 + WIDTH/30), int(HEIGHT*((3+10)/20))), color = WHITE)

	#Return player to title screen if they select it
	if (keyboard.kp_enter or keyboard.space) and option_position == 3:
		in_menu = True
		in_options = False
		clock.unschedule(options_loop)
		clock.schedule_interval(menu_loop, 1/60)

	#General moving around the options
	if keyboard.down:
		option_position += 1
		sounds.menu_select.play()
		time.sleep(0.15)
	if option_position > 3:
		option_position = 0
	if keyboard.up: 
		option_position -= 1
		sounds.menu_select.play()
		time.sleep(0.15)
		if option_position < 0:
			option_position = 3
	time.sleep(0.15)
	


def game_loop():
	global x_paddle, y_paddle, BACKGROUND_BOX, FIRST_LOOP, BALL_START
	global ball_x, ball_y, up_flag, x_incr, WIDTH, HEIGHT
	global PADDLE_WIDTH, PADDLE_HEIGHT, WIDTH, HEIGHT, ball
	global difficulty, lives, target_score, score, from_menu_first
	#Clear the screen and draw the top wall
	draw_rectangle(BACKGROUND_BOX, GREEN)
	draw_rectangle(TOP_BOX, (0,100,200))
	
	#Draw the ball and paddle where it goes the first time
	if FIRST_LOOP == True:
		if from_menu_first == True:
			ball.reset(BALL_START[0], BALL_START[1], 0)
			from_menu_first = False
			lives = LIVES_VECTOR[lives_vector_position]
		else:
			ball.reset(BALL_START[0], BALL_START[1], ball.score)

		#This also intializes the ball
		draw_image(ball.image, ball.ball_x, ball.ball_y)
		draw_image(images.paddle, paddle.x_paddle, paddle.y_paddle)
		time.sleep(1.0)
		FIRST_LOOP = False


	paddle.move_the_paddle(keyboard, WIDTH)
	ball.move_the_ball(WIDTH, paddle.x_paddle, paddle.y_paddle, paddle.PADDLE_WIDTH, keyboard, difficulty)
	draw_image(ball.image, ball.ball_x, ball.ball_y)
	draw_image(paddle.image, paddle.x_paddle, paddle.y_paddle)
	score = ball.score

	#Have the ball reset and a brief pause if it goes way off the end of the screen
	#This means the player missed it
	if ball.ball_y > int(HEIGHT * 1.2):
		#del ball #clears the ball object out
		FIRST_LOOP = True
		lives -= 1
		time.sleep(2.0)
	
	screen.draw.text("Lives: " + str(lives), (10, 5), color = WHITE)
	screen.draw.text("Score:   " + str(score) + "/" + str(target_score), (WIDTH-150, 5), color = WHITE)
	if lives == 0:
		FIRST_LOOP = True
		clock.unschedule(game_loop)
		clock.schedule_interval(lose_game, 1/30)
	
	if score >= target_score:
		FIRST_LOOP = True
		clock.unschedule(game_loop)
		clock.schedule_interval(win_game, 1/30)

	if ball.ball_sound == 2:
		ball.ball_sound = 0
		sounds.ball_low.play()
	elif ball.ball_sound == 1:
		ball.ball_sound = 0
		sounds.ball_high.play()

def lose_game():
	global x_paddle, y_paddle, BACKGROUND_BOX, FIRST_LOOP, BALL_START
	global ball_x, ball_y, up_flag, x_incr, WIDTH, HEIGHT
	global PADDLE_WIDTH, PADDLE_HEIGHT, WIDTH, HEIGHT, ball
	global difficulty, lives, target_score, LIVES_VECTOR, lives_vector_position, score
	
	#Clear the screen and draw the top wall
	
	draw_rectangle(BACKGROUND_BOX, GREEN)
	draw_rectangle(TOP_BOX, (0,100,200))
	screen.draw.text("Lives: " + str(lives), (10, 10), color = WHITE)
	screen.draw.text("Score: " + str(score) + "/" + str(target_score), (WIDTH-100, 10), color = WHITE)
	draw_image(paddle.image, paddle.x_paddle, paddle.y_paddle)
	screen.draw.text("GAME", (int(WIDTH/3.5), int(HEIGHT/2) - int(HEIGHT/10)), color = WHITE, fontsize=36)
	screen.draw.text("OVER!", (int(WIDTH/3.5), int(HEIGHT/2)), color = WHITE, fontsize=36)
	screen.draw.text("Hit space to return to the title screen.", (int(WIDTH/3.5), int(HEIGHT/2) + int(HEIGHT/10)), color = WHITE)
	

	if keyboard.space:
		clock.unschedule(lose_game)
		clock.schedule_interval(menu_loop,1/60)
		time.sleep(0.25)

def win_game():
	global x_paddle, y_paddle, BACKGROUND_BOX, FIRST_LOOP, BALL_START
	global ball_x, ball_y, up_flag, x_incr, WIDTH, HEIGHT
	global PADDLE_WIDTH, PADDLE_HEIGHT, WIDTH, HEIGHT, ball
	global difficulty, lives, target_score, LIVES_VECTOR, lives_vector_position, score
	
	#Clear the screen and draw the top wall
	
	draw_rectangle(BACKGROUND_BOX, GREEN)
	draw_rectangle(TOP_BOX, (0,100,200))
	screen.draw.text("Lives: " + str(lives), (10, 10), color = WHITE)
	screen.draw.text("Score: " + str(score) + "/" + str(target_score), (WIDTH-100, 10), color = WHITE)
	draw_image(paddle.image, paddle.x_paddle, paddle.y_paddle)
	screen.draw.text("CONGRATULATIONS!", (int(WIDTH/3), int(HEIGHT/2) - int(HEIGHT/10)), color = WHITE, fontsize=36)
	screen.draw.text("YOU BEAT JOE PONG!", (int(WIDTH/3), int(HEIGHT/2)), color = WHITE, fontsize=36)
	if difficulty < 15:
		screen.draw.text("Try again on a harder difficulty!", (int(WIDTH/3), int(HEIGHT/2) + int(HEIGHT/10)), color = WHITE, fontsize=36)
	elif difficulty >= 15 and target_score == 100 and LIVES_VECTOR[lives_vector_position] == 1:
		screen.draw.text("There are none as good as you! WAY TO GO!", (int(WIDTH/3), int(HEIGHT/2) + int(HEIGHT/10)), color = WHITE, fontsize=36)
		screen.draw.text("Written by Joseph Garland. @soptheturtle on twitter", (int(WIDTH/3), int(HEIGHT*8/10)), color = WHITE, fontsize=24)
	elif difficulty >= 15:
		screen.draw.text("You sure are good at this. But can you take it to the next level?", (int(WIDTH/15), int(HEIGHT/2) + int(HEIGHT/10)), color = WHITE, fontsize=36)
	else:
		print('Nothing to show see here')
	screen.draw.text("Hit spacebar to return to the title screen.", (int(WIDTH/3), int(HEIGHT/2) + int(HEIGHT*2/10)), color = WHITE)
	
	#Reset the lives and the score
	lives = LIVES_VECTOR[lives_vector_position]

	if keyboard.space:
		clock.unschedule(win_game)
		clock.schedule_interval(menu_loop,1/30)
		time.sleep(0.25)


#Game starts with this
clock.schedule_interval(menu_loop, (1/30))

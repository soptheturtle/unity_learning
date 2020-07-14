#Spacewalk
#by Joseph Garland
#www.sean.co.uk / www.nostarch.com

WIDTH = 800
HEIGHT = 600
player_x = 675
player_y = 80
def draw():
	screen.blit(images.backdrop, (0,0))
	screen.blit(images.mars, (50,50))
	screen.blit(images.astronaut, (player_x, player_y))
	screen.blit(images.ship, (650,75))

def game_loop():
	global player_x, player_y
	if keyboard.right:
		player_x += 5
	elif keyboard.left:
		player_x -= 5
	elif keyboard.up:
		player_y -= 5
	elif keyboard.down:
		player_y += 5

clock.schedule_interval(game_loop, 0.03)
	
	

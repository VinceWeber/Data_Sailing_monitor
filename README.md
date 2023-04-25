# POHODA PERF
This personnal project was dedicated, to improve my knowledge of sailing data, applied in regatta : 

1- To create a BUS software of sailing data (GPD, wind, autopilot, Sea) encoded in NMEA183 protocol before transfering data to the navigation software.
2- Correct data sensors if needed (for example to perform calibration).
3- Directly compute some data of navigation for example: 
- Wind, speed and direction: True, Apparent and Geographic wind + store history of last 30 min.
- Current
- Ratio between boat speed and theoritical speed according to wheather conditions
- 4 - Create my own polar (theoriticalspeed) and record boat settings.


# Table of contents

- [Usage](#usage)
- [Recommended configurations](#recommended-configurations)
- [Project status](#Project-status)


# Usage
[(Back to top)](#table-of-contents)

A main form where you can :
		-> start/stop listenning and send NMEA sentences on Serial port.
		-> Select to show or hide others forms (Valeurs / Enr. perf / Reglages )
		-> Modify sensors data
		->Define some parameters :
			-> Bus vers com1 : if you want to send data to others devices (software / autopilot )
			-> some time parameters of the program
			
	![image]('image path _ Main Form')
			
A second form to read data :
		-> From sensors (on the left)
		-> Computed by the software (on the right of the form).
	
	![image]('image path _ valeurs ')
	
A third form to record data in a text file :
		-> Boat Settings has to be filled manually on the form
		-> Boat navigation values are given by calculus.
		-> Informations are stored in a text file, when button save is pressed.
	
	![image]('image path _ record ')


# Recommended configurations
[(Back to top)](#table-of-contents)

Was working on windows XP, with framework .net 4.0


# Project status
[(Back to top)](#table-of-contents)

   Project is no more active since 2011, since QTVLM software became well known.


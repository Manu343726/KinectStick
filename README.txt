KinectStick
===========

Atajos de teclado por voz para videojuegos

Manu343726 2012

Cualquiera que desee utilizar el código/programa es libre de hacerlo,
pero no doy ninguna garantía de funcionamiento.

Para cualquier duda o sugerencia, se puede contactar conmigo a través
del mi cuenta de twitter (@Manu343726) o el mismo repositorio del
proyecto.

También suelo frecuentar los foros de Stratos-AD
(http://www.stratos-ad.com/)


#Requisitos de funcionamiento:

El programa ha sido desarrollado sobre .NET Framework 4.5. Asímismo, el sistema de reconocimiento de voz
utiliza el stream de audio de kinect como fuente.
En concreto, está desarrollado sobre la versión 1.6 del SDK de kinect para windows 
http://www.microsoft.com/en-us/kinectforwindows/develop/developer-downloads.aspx)

Además de es necesario el SDK de Microsoft Speech Plattaform (http://www.microsoft.com/en-us/download/details.aspx?id=14373),
con el la versión del runtime en español (http://www.microsoft.com/en-us/download/details.aspx?id=3971)

Por último, el programa permite tanto la generación de atajos de teclado como la utilización de un joystick virtual.
Para los atajos de teclado, he utilizado un pequeño wrapper llamado "InputSimulator" que encapsula la función "SendInput" de la
API de WIndows. Se puede descaragar desde aquí: http://inputsimulator.codeplex.com/

Para el joystick virtual he utilizado el SDK de vJoy (http://headsoft.com.au/index.php?category=vjoy)
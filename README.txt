KinectStick
===========

Atajos de teclado por voz para videojuegos

Manu343726 2012

Cualquiera que desee utilizar el c�digo/programa es libre de hacerlo,
pero no doy ninguna garant�a de funcionamiento.

Para cualquier duda o sugerencia, se puede contactar conmigo a trav�s
del mi cuenta de twitter (@Manu343726) o el mismo repositorio del
proyecto.

Tambi�n suelo frecuentar los foros de Stratos-AD
(http://www.stratos-ad.com/)


#Requisitos de funcionamiento:

El programa ha sido desarrollado sobre .NET Framework 4.5. As�mismo, el sistema de reconocimiento de voz
utiliza el stream de audio de kinect como fuente.
En concreto, est� desarrollado sobre la versi�n 1.6 del SDK de kinect para windows 
http://www.microsoft.com/en-us/kinectforwindows/develop/developer-downloads.aspx)

Adem�s de es necesario el SDK de Microsoft Speech Plattaform (http://www.microsoft.com/en-us/download/details.aspx?id=14373),
con el la versi�n del runtime en espa�ol (http://www.microsoft.com/en-us/download/details.aspx?id=3971)

Por �ltimo, el programa permite tanto la generaci�n de atajos de teclado como la utilizaci�n de un joystick virtual.
Para los atajos de teclado, he utilizado un peque�o wrapper llamado "InputSimulator" que encapsula la funci�n "SendInput" de la
API de WIndows. Se puede descaragar desde aqu�: http://inputsimulator.codeplex.com/

Para el joystick virtual he utilizado el SDK de vJoy (http://headsoft.com.au/index.php?category=vjoy)
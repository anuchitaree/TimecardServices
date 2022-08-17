https://docs.microsoft.com/en-us/answers/questions/256664/is-it-possible-to-create-a-setup-filemsi-in-visual.html


Power shell
sc.exe create TaffTimeCard binpath=C:\Temp\workerservice\TimecardServices.exe start=auto
sc.exe description TaffTimeCard ""
sc.exe delete TaffTimeCard


sc.exe create TaffTimecard binpath= C:\TaffTimecard\TimecardService.exe start=auto displayname="Taff Timecard" password="4911"
sc.exe description TaffTimecard "Transfer all text files every 60 second to Postgresql and send data to mp_timecard database, you setup scantime"






sc.exe [<servername>] create [<servicename>]  
[start= {boot | system | auto | demand | disabled | delayed-auto}] 
[error= {normal | severe | critical | ignore}] 
[binpath= <binarypathname>] [group= <loadordergroup>]
[tag= {yes | no}] 
[depend= <dependencies>] 
[obj= {<accountname> | <objectname>}] 
[displayname= <displayname>]
[password= <password>]


sc.exe create TaffTimecard binpath= C:\TaffTimecard\workers\TimecardServices.exe start=auto

C:\Taff Timecard\workerservice
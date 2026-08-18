#align(center, text("Midterm Activity", 27pt))
#grid(
  columns: (1fr, 1fr),
  align(left)[
    Marlon Alen I Flores\
    BSCS - 3A
  ],
  align(right)[
    CS311\
    #datetime(year: 2026, month: 8, day: 18).display()
  ],
)

#outline(depth: 1)

#show raw: set text(font: "JetBrainsMono NF")
#show link: set text(fill: blue)
#import "@preview/codly:1.3.0": *
#import "@preview/codly-languages:0.1.1": *
#import "@preview/cetz:0.4.2"
#show: codly-init.with()
#codly(languages: codly-languages, stroke: 0.3pt + black)
#show raw.where(block: true, lang: "console"): it => local(
  header: text(fill: black, font: "New Computer Modern", [*Output*]),
  header-cell-args: (fill: luma(240)),
  number-format: none,
  zebra-fill: none,
  display-icon: false,
  display-name: false,
  breakable: false,
  fill: rgb("#202032"),
  {
    show regex("^\$.*"): text.with(fill: rgb("#a6e3a1"))
    text(fill: white, it)
  },
)

= Screen Recording
#link("https://drive.google.com/file/d/1OwNRp42ogk7wdkl2SbK60TY_Zhws30Bp/view?usp=sharing")

= Scenarios
== Scene-1
Open the equipment management form, click add new equipment button, and click add/save button.
#image("./screenshots/scene-1-1")
== Scene-2
On the same add new equipment form, input a year that is a character or is less than 1000 or higher than 9999.
#image("./screenshots/scene-2-1")
#image("./screenshots/scene-2-2")
== Scene-3
On the same add new equipment form, input a serial number that is already existing.
#image("./screenshots/scene-3")
== Scene-4
Add a new equipment and select JASC on the branch, MAC on the type, serial
number of SN20250826091535, Apple INC. on the manufacturer, 2020 on year
model, select any on the department, and APPLE MAC COMPUTER on description
and click save button. Include also a screenshot of the updated equipment
management form. Include also a screenshot of the updated equipment
management form highlighting the added equipment
#image("./screenshots/scene-4-1")
#image("./screenshots/scene-4-2")
#image("./screenshots/scene-4-3")

== Scene-5
Add a new equipment and select JRC on the branch, CPU on the type, serial number
of SN20250826092155, ACER INC. on the manufacturer, 2023 on year model, select
any on the department, and ACER CPU on description and click save button. Include
also a screenshot of the updated equipment management form highlighting the
added equipment
#image("./screenshots/scene-5-1")
#image("./screenshots/scene-5-2")
#image("./screenshots/scene-5-3")

== Scene-6
Update the year model, branch, and status of the equipment on instruction 5 into
2024, ABC, ON-REPAIR respectively. Include also a screenshot of the updated
equipment management form highlighting the updated equipment.
#image("./screenshots/scene-6-1")
#image("./screenshots/scene-6-2")
#image("./screenshots/scene-6-3")

== Scene-7
Update the status of the equipment in instruction 4 into RETIRED using the retired
button on the equipment management form. Include also a screenshot of the
updated equipment management form highlighting the updated equipment.
#image("./screenshots/scene-7-1")
#image("./screenshots/scene-7-2")

== Scene-8
Update the status of the equipment in instruction 5 into ON-REPAIR using the working button on the equipment management form. Include also a screenshot of the updated equipment management form highlighting the updated equipment.
#image("./screenshots/scene-8-1")
#image("./screenshots/scene-8-2")
#image("./screenshots/scene-8-3")

== Scene-9
Update the status of the equipment in instruction 4 into RETIRED using the working button on the equipment management form.
#image("./screenshots/scene-9")

== Scene-10
Update the status of the equipment in instruction 4 into ON-REPAIR using the working button on the equipment management form.
#image("./screenshots/scene-10-1")
#image("./screenshots/scene-10-2")

== Scene-11
Search equipment with model of CPU.
#image("./screenshots/scene-11-1")
== Scene-12
Search equipment with branch of JSC.
#image("./screenshots/scene-12-1")

== Scene-13
Search equipment with status of working.
#image("./screenshots/scene-13")

== Scene-14
Delete the equipment on instruction 4. Include also a screenshot of the updated equipment management after the delete.
#image("./screenshots/scene-14-1")
#image("./screenshots/scene-14-2")

== Scene-15
Show the logs with delete that you have done
#image("./screenshots/scene-15")

= Code
Full code is available at
#link("https://github.com/Alen-Flores/CS311-2026-FLORES/tree/master/midterm/CS311MidtermActivity-Flores")

#let code(file) = [
  === #file
  #raw(block: true, lang: "C#", read(file))
]

== Models
#code("./Models/Equipment.cs")

== Services
#code("./Services/EquipmentService.cs")

== ViewModel
#code("./ViewModels/Equipments/EquipmentsViewModel.cs")
#code("./ViewModels/Equipments/AddEquipmentViewModel.cs")
#code("./ViewModels/Equipments/UpdateEquipmentViewmodel.cs")

#align(center, text("Activity-1", 27pt))
#grid(
  columns: (1fr, 1fr),
  align(left)[
    Marlon Alen I Flores\
    BSCS - 3A
  ],
  align(right)[
    CS311\
    2026-07-01
  ],
)

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


= Code
== Validators.cs
#raw(block: true, lang: "c#", read("./Validators.cs"))

= Lecture 2
- Both inputs are empty, select subtract, and click submit.
#image("./screenshots/scene-1")
- Both inputs are not numeric, select subtract, and click submit.
#image("./screenshots/scene-2")
- Both inputs are number, select subtract, and click submit.
#image("./screenshots/scene-3")
#raw(block: true, lang: "c#", read("./LectureProgram2.axaml.cs"))

= Lecture 3
- Input 1 is empty and input2 is a character/string, select subtract and multiply, and click submit.
#image("./screenshots/scene-4")
- Input 1 is a character/string and input 2 is empty, select subtract and multiply, and click submit.
#image("./screenshots/scene-5")
- Input 1 is empty and input 2 is a number, select subtract and multiply, and click submit.
#image("./screenshots/scene-6")
- Both inputs are number, select subtract and multiply, and click submit.
#image("./screenshots/scene-7")
#raw(block: true, lang: "c#", read("./LectureProgram3.axaml.cs"))

= Lecture 4
- Input 1 is a number and input 2 is empty, select divide, and click submit.
#image("./screenshots/scene-8")
- Input 1 is a character/string and input 2 is a number, select divide, and click submit.
#image("./screenshots/scene-9")
- Input 1 is a number and input 2 is a character/string, select divide, and click submit.
#image("./screenshots/scene-10")
- Both inputs are number, select divide, and click submit.
#image("./screenshots/scene-11")
#raw(block: true, lang: "c#", read("./LectureProgram4.axaml.cs"))

= Video
#link("https://drive.google.com/file/d/1Jc-CcFJevc-LhK47GU_NKpSy7lsNFQ5B/view?usp=sharing")

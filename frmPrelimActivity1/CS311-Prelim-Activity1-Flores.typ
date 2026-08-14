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
#raw(block: true, lang: "c#", read("./MainWindow.axaml.cs"))

= Screenshots

1. Opposite and Hypotenuse are empty and click sine button.
#image("screenshots/scene-1")

2. Adjacent and Hypotenuse are empty and click cosine button.
#image("screenshots/scene-2")

3. Opposite and Adjacent are empty and click tangent button.
#image("screenshots/scene-3")

4. All inputs are empty and click compute all button.
#image("screenshots/scene-4")

5. Adjacent is not a number.
#image("screenshots/scene-5")

6. Hypotenuse is not a number.
#image("screenshots/scene-6")

7. Opposite is not a number.
#image("screenshots/scene-7")

8. Hypotenuse is 3 and Opposite is 6 and click sine button.
#image("screenshots/scene-8")

9. Hypotenuse is 6 and Opposite is 3 and click sine button. Result should be in 2 decimal places only.
#image("screenshots/scene-9")

10. Adjacent is 10 and Hypotenuse is 5 and click cosine button.
#image("screenshots/scene-10")

11. Adjacent is 5 and Hypotenuse is 10 and click cosine button. Result should be in 2 decimal places only.
#image("screenshots/scene-11")

12. Adjacent is 8 and Opposite is 5 and click tangent button. Result should be in 2 decimal places only.
#image("screenshots/scene-12")

13. Adjacent is 8, Hypotenuse is 5, and Opposite is 3 and click compute all button.
#image("screenshots/scene-13")

14. Adjacent is 3, Hypotenuse is 5, and Opposite is 8 and click compute all button.
#image("screenshots/scene-14")

15. Adjacent is 5, Hypotenuse is 8, and Opposite is 3 and click compute all button. Result should be in 2 decimal places only
#image("screenshots/scene-15")

#link("https://drive.google.com/file/d/1w2RrUu6AvIIk6oHzqqRsgssAXj9rJIse/view?usp=sharing")

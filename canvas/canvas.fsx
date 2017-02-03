#r "../node_modules/fable-core/Fable.Core.dll"

open Fable.Core
open Fable.Import
open Fable.Import.Browser

let canvas =  document.getElementsByTagName_canvas().[0]
canvas.width <- 1000.
canvas.height <- 800.
let ctx = canvas.getContext_2d()
ctx.fillStyle <- U3.Case1 "rgb(200,0,0)"
ctx.fillRect (10., 20., 30., 40.)


(*
TODO: draw more stuff on the canvas...
See more about using the canvas here:
http://www.w3schools.com/html/html5_canvas.asp
*)



(*
TODO: uncomment the following block, build and run.
*)

(*
let rng (): float = JS.Math.random()

let red = rng () * 255. |> int
let green =  rng () * 255. |> int
let blue =  rng () * 255. |> int

let color = sprintf "rgb(%i,%i,%i)" red green blue 
ctx.fillStyle <- U3.Case1 color
ctx.fillRect (10., 10., 200. * rng (), 200. * rng ())
*)



(*
TODO: uncomment the following block, build and run.
*)

(*
// wait time between the screen update
let waitFor = 100. // milliseconds

// loop: draw a rectange of given width and height,
// increase width and height, wait for 100 ms, and repeat.
let rec loop (width,height) () =

    ctx.fillStyle <- U3.Case1 "rgb(0,0,200)"
    ctx.fillRect (10., 10., width, height)

    let height = height + 1.
    let width = width + 1.

    window.setTimeout(loop (width,height), waitFor) |> ignore

// start the loop
loop (1.,1.) ()
*)



(*
WHAT NEXT?

Well, whatever you want :)
- build something fun with this; this is what I did:
https://twitter.com/brandewinder/status/826297729311612928
- check out the samples for more demos, and find something 
that gets YOU excited: http://fable.io/samples.html

Samples illustrate things like using the keyboard, doing a 
game, Elm-style pages, WebGL, ...
*)

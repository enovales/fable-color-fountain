module App

#r "../node_modules/fable-core/Fable.Core.dll"

open Fable.Core
open Fable.Core.JsInterop
open Browser

let canvas: Browser.Types.HTMLCanvasElement = unbox(document.getElementById("canvas"))
let ctx = canvas.getContext_2d()

(*
ctx.fillStyle <- U3.Case1 "rgb(200,0,0)"
ctx.fillRect (10., 20., 30., 40.)
*)


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


let rng (): float = JS.Math.random()

(*
for i = 1 to 10 do
    for j = 1 to 10 do
        let red = rng () * 255. |> int
        let green =  rng () * 255. |> int
        let blue =  rng () * 255. |> int

        let color = sprintf "rgb(%i,%i,%i)" red green blue 
        System.Console.WriteLine("blah!!!!")
        ctx.fillStyle <- U3.Case1 color
        ctx.fillRect (10. * float i, 10. * float j, 10., 10.)
*)


// wait time between the screen update
let waitFor = 10. // milliseconds

let particleLimit = 200

type Particle = {
    x: double
    y: double
    xvel: double
    yvel: double
    c: (int * int * int)
    rot: double
    rotVel: double
}
with
    override this.ToString() = 
        let (r,g,b) = this.c
        "Particle(x = " + this.x.ToString() + ", y = " + this.y.ToString() + ", xvel = " + this.xvel.ToString() + ", yvel = " + this.yvel.ToString() + ", c = (" + r.ToString() + ", " + g.ToString() + ", " + b.ToString() + "))"

let updateParticle(dt: double)(p: Particle) = 
    {
        p with 
            x = p.x + p.xvel * dt
            y = p.y + p.yvel * dt
            yvel = p.yvel + 1. * dt
            rot = (p.rot + p.rotVel * dt) % (2. * 3.14159)
    }

let refillParticles(p: Particle array, dt: double) = 
    let stillValid = 
        p 
        |> Array.filter(fun pt -> (pt.y < 1000.))
    //System.Console.WriteLine("stillValid.Length = " + stillValid.Length.ToString())
    let updatedPos = 
        stillValid
        |> Array.map(updateParticle(dt))

    //System.Console.WriteLine("updatedPos = " + updatedPos |> Array.map(fun p -> p.ToString()).ToString())
    let toCreate = particleLimit - stillValid.Length
    //System.Console.WriteLine("going to create " + toCreate.ToString() + " particles")
    let newParticles =
        seq { 
            for i in 0..toCreate do 
                yield {
                    Particle.x = 200.
                    y = 300.
                    xvel = (rng() - 0.5) * (rng() * 30.)
                    yvel = -(rng() * 25.)
                    c = (int (rng() * 255.), int (rng() * 255.), int (rng() * 255.))
                    rot = (rng() * 2. * 3.14159)
                    rotVel = (rng() * 1.5)
                }
        } 
        |> Seq.toArray

    updatedPos |> Array.append(newParticles)

let mutable particles = [||]
let timestep = 0.5

// loop: draw a rectange of given width and height,
// increase width and height, wait for 100 ms, and repeat.
let rec loop (t) () =
    particles <- refillParticles(particles, timestep)

    ctx.clearRect(0., 0., 10000., 10000.)
    let drawParticle(p: Particle) =
        let (r,g,b) = p.c
        let fs = "rgb(" + r.ToString() + ", " + g.ToString() + ", " + b.ToString() + ")"
        ctx.fillStyle <- U3.Case1 fs

        let x1 = (p.x - 5.)
        let x2 = (p.x + 5.)
        let y1 = (p.y - 5.)
        let y2 = (p.y + 5.)

        (*
        //System.Math.Cos(p.rot)
        let x1 = (p.x - (5. * System.Math.Cos(p.rot))
        let x2 = (p.x + (5. * System.Math.Cos(p.rot))
        let y1 = (p.y - (5. * System.Math.Sin(p.rot))
        let y2 = (p.y + (5. * System.Math.Sin(p.rot))
        *)

        //ctx.fillRect(x1, y1, 10., 10.)
        ctx.beginPath()
        ctx.moveTo(x1, y1)
        ctx.lineTo(x2, y1)
        ctx.lineTo(x2, y2)
        ctx.lineTo(x1, y2)
        ctx.lineTo(x1, y1)
        ctx.closePath()
        ctx.fill()

    particles
    |> Array.iter drawParticle

    let nextT = t + timestep
    window.setTimeout(loop nextT, int(waitFor))|> ignore

// start the loop
loop(0.)()

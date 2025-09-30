This is an unfinished tutorial for using the Manual Screen Editor.

# Example 1: Filling gaps in objects

Sometimes, often due to extreme camera positions or errosion effects, objects might appear broken in ways the modder did not intent.
In this example, we will see how a modder might fix such a blemish.

1. Import the rendered png that you want to fix.

You can do this either by clicking the file expolorer button across from
the label "Rendered" or by dragging and droping the png file from your file explorer to anywhere in the Manual Screen Editor main window.

![eg1_1](/docs/figures/eg1_1.png?raw=true)

2. Click the Decompose button.

This should be done every time you import a rendered layer. Without this step, you will not be able to access the base components of a screen.

![eg1_2](/docs/figures/eg1_2.png?raw=true)

3. Select the view mode you want to use.

Do this by left clicking one of the 10 square layer buttons above the canvas.
In this case, the depth, level color, sky and rendered layers give us useful information about this particular broken object. This example will continue with rendered layer viewing for clairity.

![eg1_3](/docs/figures/eg1_3.png?raw=true)

4. Zoom into the targeted area.

Zoom in and out by holding Ctrl and scrolling with the mouse wheel. Vertical and horizontal scrolling can be done by holding nothing and shift respectively while scrolling the mouse wheel.

![eg1_4](/docs/figures/eg1_4.png?raw=true)

5. Use the eye dropper tool to highlight a pixel from the area that you want to extend.

Click the eye dropper button. When the eye dropper tool is selected it will highlight itself in blue. Once used, it will automatically turn itself off. Use the eye dropper tool by left clicking any pixel from the canvas.

![eg1_5](/docs/figures/eg1_5.png?raw=true)

6. Shift+left click the layer buttons you want to modify and ensure they are shaded gray.

Here we want to replace sky pixels. That means that we need to supply information about the depth, level color, light and of course sky components. By marking the associated layers for editing, we are able to change only the information related to those components within the pixels we paint. All other information contained in layers we did not select will be preserved.

![eg1_6](/docs/figures/eg1_6.png?raw=true)

7. left click on the canvas, paiting the pixels you want to fill.

If you make a mistake, you can use Ctrl-Z to undo it.

![eg1_7](/docs/figures/eg1_7.png?raw=true)

8. Once you have made the fixes you want, use File > Save As to save the modifed screen.

Ensure that your screen has the correct name once placed in the game files.

![eg1_8](/docs/figures/eg1_8.png?raw=true)

# Example 2: Adding a neon sign

A neon sign is a good example of effect colors in use. In this example, we will make our own sign.

1. Import the rendered png that you want to fix and click the Decompose button.

Thus far, these steps are the same as in example 1.

![eg2_1](/docs/figures/eg2_1.png?raw=true)

2. Select the view mode you want to use.

In this example, we can choose to view either effect color, effect shading or rendered layers as any of these three tell us information about the sign we want to make.

3. Shift + left-click the effect color and effect shading layers to edit them.

Whenever using effect colors, you will likely want to edit both of these layers together. If you are painting over sky, you will want to also edit the sky, depth and level color layers.

4. Set the effect color and effect shading that you want to use.

Here we will use effect color A and a maximum intensity value for effect shading. Choose these values by interacting with the column of paint buttons in the left panel.
Effect color can be customized by cycling the value with each click.
Effect shading produces a pop-up window which prompts the user for a color selection, but only saves the HSB brightness of the color. Black represents an intensity of 0 (equivalent to no effect color) while white represents full intensity (the effect color completely replaces the natural color of the pixel.

![eg2_2](/docs/figures/eg2_2.png?raw=true)

5. Left click on the canvas to make your neon sign.

You can also use the Pen Size property in the left panel to change the radius of the pen.
Remember, if you make a mistake while drawing, you can use Ctrl-Z to undo your last stroke.

![eg2_3](/docs/figures/eg2_3.png?raw=true)

6. Preview your sign by loading a palette and chosing assigning a color to effect color A. 

Add a palette to Manual Screen Editor by either clicking View>Set Palette or dragging and dropping a palette onto the main program window.
Enable palette previewing by checking View>Preview Palette which will visually replace the rendered layer with a palette preview.
Assign a color to effect color A by chosing a color from the dropdown list View>Effect A.

![eg2_4](/docs/figures/eg2_4.png?raw=true)

7. Once you are happy with your sign, use File > Save As to save the modifed screen.

> Ensure that your screen has the correct name once placed in the game files.

# Example 3: Making graffiti

Here we're getting more complicated. Building off of the previous 2 examples, we should be able to start from here:

![eg3_1](/docs/figures/eg3_1.png?raw=true)

In this example, we want to add our own graffiti to this wall. How? Using index colors.

1. Edit the index color layer (above the canvas) and click on the index color paint button (on the left panel).

The index color paint button opens a new window with 255 swatches. Each swatch represents a single color index.
Left clicking a swatch will modify its color. Shift + left-clicking multiple swatches will select them for deletion (black border) which is triggered by pushing the Delete button. Ctrl + left-clicking a single swatch will add it to our pen tool and highlight it with a red border.

![eg3_2](/docs/figures/eg3_2.png?raw=true)

2. Add a color then click Ok to close the window.

Here we will add green and leave the rest of the colors.

![eg3_3](/docs/figures/eg3_3.png?raw=true)


3. Switch to the index color view and paint the graffiti.

Palette previewing cannot represent index colors accurately without rebuilding every time a change is made. For this reason, it is easier to only view the index colors. As an aside, to rebuild the palette preview, toggle View>Palette Preview off then back on. Paint the index colors similar to how we did with the effect colors. Undo might not work when painting over transparent index color pixels. To erase, instead, click the E button next to the main index color paint button.

![eg3_4](/docs/figures/eg3_4.png?raw=true)

4. (Optional) Modify a pre-existing index color

Let's say that we decided that we didn't want our graffiti to be green. 

![eg3_5](/docs/figures/eg3_5.png?raw=true)

We can always reopen the index color window and modify it.

![eg3_6](/docs/figures/eg3_6.png?raw=true)

# Example 4: Exporting/Importing component layers

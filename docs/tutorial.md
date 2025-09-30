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
1. Import the rendered png that you want to fix.
2. Click the Decompose button. This should be done every time you import a rendered layer.
3. Select the view mode you want to use (Effect color, Effect color shading, rendered).
4. Zoom into the targeted area
5. Set the effect color and intensity that you want to use.
6. Shift+left click the effect color and effect color shading layer buttons and ensure they are shaded gray.
7. left click on the canvas, paiting the pixels you want to fill.

> If you make a mistake, you can use Ctrl-Z to undo it.

8. Once you have made the additions you want, use File > Save As to save the modifed screen.

> Ensure that your screen has the correct name once placed in the game files.

# Example 3: Making graffiti


# Example 4: Exporting/Importing component layers

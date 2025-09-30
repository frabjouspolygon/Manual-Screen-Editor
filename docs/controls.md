# Shortcuts
- Undo: `Ctrl` + `Z`
- Redo: `Ctrl` + `Shift` + `Z`

# Menu

## File
- Save: Saves the rendered layer to the pre-established location specified by Save As.
- Save As: Saves the rendered layer to a new location and saves the output directory for future use.
- Save As Copy: Saves the rendered layer to a new location without updating the output directory.
- Export Layers: Exports all component layers (excluding rendered layer) to the output directory using modified names of the format
"`FileName`_`LayerName`.png"
## Edit
- Undo: Reverts the previous tool strokes (can have unexpected behavior if components and render are out of sync).
- Redo: Reinstates tool strokes that were reverted by Undo.

## View
- View Palette: Toggles if the rendered layer preview should be replaced by a palette preview.
- Set Palette: Prompts the user for which palette to use for the preview.
- Effect A: Prompts the user for a custom color to use in the palette preview for effect color A.
- Effect B: Prompts the user for a custom color to use in the palette preview for effect color B.

## About
- Displays the version of Manual Screen Editor

# Tool Panel
The tool panel, located on the left, houses controls associated with tools (pen, eyedropper) and operations (compose, decompose). Each component layer is
grouped with the layer name (ie. Depth), the file path used to load the layer if used, a file expolorer button to browse for the layer and the assoicated "paint"
data for tools. Because multiple layers can be written to at once, tools use 9 "paints" simultaneously unlike other drawing applications which only use
1 color at a time.

The controls for the paints behave as follows:
- Depth: Accepts integer depths between 1 (nearest) and 30 (farthest)
- Effect color: Cycles between `Effect Color Off` (erase), 'Effect Color A` , 'Effect Color B` and 'Effect Color Batfly Hive` (white color exclusive to swarm rooms)
(Dark varients of each setting exist however they have no visual difference from their lighter versions in-game.
- Color Index: Two buttons. One for selecting and managing index colors in a pop-up window and another, labled `E`, for ereaseing index colors.
- Level color: Cycles between `Dark`, `Light` and `Neutral` (These corespond to the red, blue and green false colors used in level editor tiles/props).
- Light: Cycles between `Shadow` and `Sunlight` (analog to "Light" tab found in most level editors).
- Pipes: Cycles between `No Pipes` (erase), `Pipe Layer 1` (nearest, depths 1-10), `Pipe Layer 2` (depths 11-20) and `Pipe Layer 3` (furthest, depths 21-30).
- Grime: Cycles between `Grime Off` (erase) and `Grime On`.
- Effect Shading: Opens a full color picker dialog box, however only the HSB brightness is kept as an integer in the inclusive range from 0 to 255.
Acts as effect color intensity.
- Sky: Cycles between `Geometry` (not sky) and `Sky`.

Tool specific controls:
- Pen Size: Sets the radius of the pen tool. For example, 1 means a pen size of 1 pixel while 2 means a circle with a diameter of 3 pixels.
- Eye Dropper: Selects the eye dropper tool and highlights the button in blue until a room screen pixel is selected. It copies component data from the targetted
pixel to the 9 paints.

Layer sync operations:
- Decompose: Processes a rendered layer and populates the component layers. This is a prerequisite for editing layers properly.
- Compose: Assembles component layers into a rendered layer. This is only after components are imported from files since tools already
write to the rendered layer.

# Layer Panel
The layer panel is located in the top right of the program window, below the menu strip and above the Canvas Panel. It controls which layers tools are allowed to
edit and which layers the canvas panel should display.

On the left of the layer panel is two numeric inputs for masking by specific depths (depths referring to the same values as seen with the Depth layer).
Leaving the values at 1 and 30 effectively disables depth masking, while shrinking their spanned range limits which pixels tools can write to.

To the right are 10 buttons for editing and previewing layers. The leftmost 9 buttons are for components and can be left clicked to be previewed
(applies a blue border to the active viewing layer) or `Shift` + left clicked to toggle if tools should edit those components
(layers which are editable have their buttons shaded gray). The rightmost button is for the rendered layer (or palette preview if the associated setting is active)
and can only be selected for viewing, not direct editing).

The furthest right element of the layer panel corresponds to cursor coordiates in the format (x,y). These coordinates are relative to the pixels of the PNG's being
edited with (0,0) corresponding to the images' top left pixel.

# Canvas Panel
The canvas panel is below the layer panel, on the right side of the application window. This panel is where the room's screen is edited.
Only a single screen can edited by the program at a time.
Pixels can be edited by left clicking on them while the desired layers are marked as editable.
Attempting to paint without any active editing layers will yield effect.
The canvas can be zoomed into or out of by scrolling with `Ctrl` + `Mouse Wheel`.
The canvas can also be scrolled vertically or horizontally with `Mouse Wheel` or `Shift` + `Mouse Wheel` respectively.
There are also two scroll bars on the right and bottom edges of the canvas that can be used as an alternative way to scroll.

# Drag and Drop
Files can be loaded by the program by dragging any number of files onto the main application window at any time. Note that images must be PNGs and of a specific naming convention to load properly. Rendered screen file names must end in and underscore `_` followed by any number of numeric characters and `.png`. Component layers must have the same naming convention used for outputting them. Drag and drop palette importing needs to follow the same naming convention for palettes as used by the game: `palete` followed by numeric characters and `.png`.

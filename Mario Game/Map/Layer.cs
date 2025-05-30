using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Layer
{
    private int display_tilesize;
    private int num_tile_per_row;
    private int pixel_tilesize;
    private string filepath;
    private Dictionary<Vector2, int> tile_array;
    private List<Rectangle> redRectangles;  // List to store red rectangles
    private Texture2D textureAtlas;
    private Vector2 cameraPosition;

    public Layer(int display_tilesize, int num_tile_per_row, int pixel_tilesize, string filepath)
    {
        this.display_tilesize = display_tilesize;
        this.num_tile_per_row = num_tile_per_row;
        this.pixel_tilesize = pixel_tilesize;
        this.filepath = filepath;
        this.tile_array = new Dictionary<Vector2, int>();
        this.redRectangles = new List<Rectangle>();
    }

    // Load the layer from the CSV file
    public void LoadLayer()
    {
        StreamReader reader = new(filepath);

        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(',');
            for (int x = 0; x < items.Length; x++)
            {
                if (int.TryParse(items[x], out int value))
                {
                    if (value > -1)
                    {
                        tile_array[new Vector2(x, y)] = value;
                    }
                }
            }
            y++;
        }
    }


    public void GenerateRedRectangles(Texture2D textureAtlas, Vector2 cameraPosition)
    {

        redRectangles.Clear();


        foreach (var item in tile_array)
        {
            // Calculate the destination vector for the tile
            Vector2 destVector = new Vector2(
                item.Key.X * display_tilesize, 
                item.Key.Y * display_tilesize
            );


            if ((item.Value == 112 || item.Value == 98 || item.Value == 114 || (item.Value >= 38 && item.Value <= 55))
        && item.Value != 6 && item.Value != 26 && item.Value != 42)
            {

                Rectangle redRect = new Rectangle(
                    (int)destVector.X,
                    (int)destVector.Y,
                    display_tilesize,  
                    display_tilesize
                );


                redRectangles.Add(redRect);

            }
        }

    }

    // Modify Draw method to ensure rectangles are generated
    public void Draw(SpriteBatch spriteBatch, Texture2D textureAtlas, Vector2 cameraPosition)
    {
        // Always generate rectangles before drawing
        GenerateRedRectangles(textureAtlas, cameraPosition);

        foreach (var item in tile_array)
        {
            // Calculate the destination vector for the tile
            Vector2 destVector = new Vector2(
                item.Key.X * display_tilesize - cameraPosition.X,
                item.Key.Y * display_tilesize - cameraPosition.Y
            );

            // Create destination rectangle from the destination vector
            Rectangle destRect = new Rectangle(
                (int)destVector.X,
                (int)destVector.Y,
                display_tilesize,
                display_tilesize
            );

            // Calculate the source rectangle for the tile in the texture atlas
            int x = item.Value % num_tile_per_row;
            int y = item.Value / num_tile_per_row;

            Rectangle src = new(
                x * pixel_tilesize,
                y * pixel_tilesize,
                pixel_tilesize,
                pixel_tilesize
            );


            spriteBatch.Draw(textureAtlas, destRect, src, Color.White);
        }


        // Draws red rectangles for debugging collisions 
        //foreach (Rectangle redRect in redRectangles)
        //{
        //    // Adjust rectangle position based on camera
        //    Rectangle adjustedRect = new Rectangle(
        //        redRect.X,
        //        redRect.Y,
        //        redRect.Width,
        //        redRect.Height
        //    );

        //    // Draw red rectangles with some transparency for visibility
        //    spriteBatch.Draw(textureAtlas, adjustedRect, Color.Red * 0.5f);
        //}
    }

    // Function to return the list of red rectangles for collision handling
    public List<Rectangle> GetRedRectangles()
    {
        // If no rectangles, force generation
        if (redRectangles.Count == 0)
        {
            GenerateRedRectangles(null, Vector2.Zero);
        }
        return redRectangles;
    }
}
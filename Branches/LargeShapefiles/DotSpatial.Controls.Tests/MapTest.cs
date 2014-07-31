﻿using DotSpatial.Projections;
using System.Collections.Generic;
using DotSpatial.Symbology;
using NUnit.Framework;

namespace DotSpatial.Controls.Tests
{
    /// <summary>
    ///This is a test class for MapTest and is intended
    ///to contain all MapTest Unit Tests
    ///</summary>
    [TestFixture]
    public class MapTest
    {
        /// <summary>
        /// A test to find out whether the default projection of a new map is WGS84.
        /// </summary>
        [Test]
        public void DefaultProjectionIsWgs84Test()
        {
            Map map = new Map();
            Assert.IsNotNull(map.Projection);
            Assert.AreEqual(map.Projection, KnownCoordinateSystems.Geographic.World.WGS1984);
        }

        /// <summary>
        /// A test to find out if the ProjectionChanged() event fires when the ProjectionEsriString
        /// property of the map is changed.
        /// </summary>
        [Test]
        public void ProjectionChangedEventFireTest()
        {
            bool eventIsFired = false;
            
            Map map = new Map();
            map.ProjectionChanged += delegate {
                eventIsFired = true;
            };

            const string esri = "GEOGCS[\"GCS_North_American_1983\",DATUM[\"D_North_American_1983\",SPHEROID[\"GRS_1980\",6378137,298.257222101004]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.0174532925199433]]";
            map.ProjectionEsriString = esri;

            Assert.IsTrue(eventIsFired, "the ProjectionChanged event should be fired when Map.ProjectionEsriString is changed.");
        }

        /// <summary>
        /// Test if the new GetAllLayers() method returns the correct number of layers if the map has groups
        /// </summary>
        [Test]
        public void GetAllLayersTest()
        {
            var map = new Map();
            var group = new MapGroup();
            map.Layers.Add(group);
            group.Layers.Add(new MapPolygonLayer());
            group.Layers.Add(new MapLineLayer());
            group.Layers.Add(new MapPointLayer());

            //add a nested group
            var group2 = new MapGroup();
            group.Layers.Add(group2);
            group2.Layers.Add(new MapPointLayer());
            group2.Layers.Add(new MapLineLayer());
            group2.Layers.Add(new MapPolygonLayer());

            List<ILayer> layerList = map.GetAllLayers();
            Assert.AreEqual(layerList.Count, 6);
        }

        /// <summary>
        /// Test if the new GetAllLayers() method returns the correct number of layers if the map has groups
        /// </summary>
        [Test]
        public void GetAllGroupsTest()
        {
            var map = new Map();
            var group = new MapGroup();
            map.Layers.Add(group);
            group.Layers.Add(new MapPolygonLayer());
            group.Layers.Add(new MapLineLayer());
            group.Layers.Add(new MapPointLayer());

            //add a nested group
            var group2 = new MapGroup();
            group.Layers.Add(group2);
            group2.Layers.Add(new MapPointLayer());
            group2.Layers.Add(new MapLineLayer());
            group2.Layers.Add(new MapPolygonLayer());

            List<IMapGroup> groupList = map.GetAllGroups();
            Assert.AreEqual(groupList.Count, 2);
        }
    }
}
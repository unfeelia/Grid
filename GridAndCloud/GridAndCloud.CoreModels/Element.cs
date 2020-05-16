using GridAndCloud.CoreModels.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Attribute = GridAndCloud.CoreModels.Attributes.Attribute;

namespace GridAndCloud.CoreModels
{
    public class Element
    {
        private Model _model;

        public Model Model { get => _model; }

        public int Id { get; set; }

        private IDictionary<int, int> _attributesValues;

        public IEnumerable<(int, Attribute, int)> Attributes => _model.Attributes.Join(_attributesValues, 
            x => x.Key, 
            y => y.Key,
            (x,  y) => (x.Key, x.Value, y.Value));

        public Element(Model model)
        {
            _model = model;

            _attributesValues = new Dictionary<int, int>();
            
            var keys = _model.Attributes.Select(x => x.Key);
            
            foreach(var key in keys)
            {
                _attributesValues.Add(key, 0);
            }
        }

        public Element(Model model, IDictionary<int, int> attributes)
        {
            _model = model;

            _attributesValues = new Dictionary<int, int>();

            var keys = _model.Attributes.Select(x => x.Key);

            foreach (var key in keys)
            {
                _attributesValues.Add(key, attributes[key]);
            }
        }

        public void SetAttribute(int attributeId, int value)
        {
            _attributesValues[attributeId] = value;
        }

        public ValueType GetAttribute(int attributeId)
        {
            return _attributesValues[attributeId];
        }

        public static bool IsTheSameField(Element a, Element b)
        {
            return a._model.Equals(b._model);
        }
    }
}
